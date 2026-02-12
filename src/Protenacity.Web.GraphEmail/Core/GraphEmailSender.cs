using Azure.Identity;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Identity.Client;
using MimeKit;
using MimeKit.IO;
using Protenacity.Web.GraphEmail.Extensions;
using System.Net.Mail;
using Umbraco.Cms.Core.Configuration.Models;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Mail;
using Umbraco.Cms.Core.Models.Email;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Infrastructure.Extensions;

namespace Protenacity.Web.GraphEmail.Core;

public class GraphEmailSender(ILogger<GraphEmailSender> logger,
        IOptions<GlobalSettings> globalSettings,
        IOptions<Configuration.Graph> graphSettings,
        IEventAggregator eventAggregator,
        IHttpContextAccessor httpContextAccessor,
        INotificationHandler<SendEmailNotification>? handlerSync = null,
        INotificationAsyncHandler<SendEmailNotification>? handlerAsync = null) : IEmailSender
{
    private bool NotificationHandlerRegistered => handlerSync is not null || handlerAsync is not null;

    public async Task SendAsync(EmailMessage message, string emailType) =>
        await SendAsyncInternal(message, emailType, false);

    public async Task SendAsync(EmailMessage message, string emailType, bool enableNotification) =>
        await SendAsyncInternal(message, emailType, enableNotification);

    public bool CanSendRequiredEmail() => 
        globalSettings.Value.IsSmtpServerConfigured || 
        globalSettings.Value.IsPickupDirectoryLocationConfigured || 
        NotificationHandlerRegistered || 
        !string.IsNullOrWhiteSpace(GraphConfig.Value.TenantId);

    private Lazy<Configuration.Graph> GraphConfig = new Lazy<Configuration.Graph>(() =>
    {
        return new Configuration.Graph
        {
            TenantId = string.IsNullOrWhiteSpace(graphSettings.Value.TenantId) ? Environment.GetEnvironmentVariable("TenantId") : graphSettings.Value.TenantId,
            ClientId = string.IsNullOrWhiteSpace(graphSettings.Value.ClientId) ? Environment.GetEnvironmentVariable("ClientId") : graphSettings.Value.ClientId,
            ClientSecret = string.IsNullOrWhiteSpace(graphSettings.Value.ClientSecret) ? Environment.GetEnvironmentVariable("ClientSecret") : graphSettings.Value.ClientSecret,
            FromEmailUser = (string.IsNullOrEmpty(graphSettings.Value.FromEmailUser) ? globalSettings.Value.Smtp?.From : graphSettings.Value.FromEmailUser)
        };
    });

    private string FromUser(EmailMessage message, string? defaultValue)
    {
        if (!string.IsNullOrWhiteSpace(message.From))
        {
            return message.From;
        }

        if (!string.IsNullOrWhiteSpace(defaultValue))
        {
            return defaultValue;
        }

        var host = httpContextAccessor.HttpContext?.Request?.Host;

        if (host.HasValue && !string.IsNullOrWhiteSpace(host.Value.Host))
        {
            return "noreply@" + host.Value.Host;
        }

        return "noreply@unknown.com";
    }

    private async Task SendAsyncInternal(EmailMessage message, string emailType, bool enableNotification)
    {
        try
        {
            if (message?.To?.Any() != true)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (enableNotification)
            {
                var notification = new SendEmailNotification(message.ToNotificationEmail(globalSettings.Value.Smtp?.From), emailType);
                await eventAggregator.PublishAsync(notification);

                // if a handler handled sending the email then don't continue.
                if (notification.IsHandled)
                {
                    logger.LogDebug(
                        "The email sending for {Subject} was handled by a notification handler",
                        notification.Message.Subject);
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(GraphConfig.Value.TenantId))
            {
                await SendAsyncInternalSmtp(message);
            }
            else
            {
                await SendAsyncInternalGraph(message);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to send email");
            return;
        }
    }

    private async Task SendAsyncInternalGraph(EmailMessage message)
    {
        logger.LogInformation($"Trying to send \'{message.Subject}\' email to {message.To.ToString()} using Graph with TenantId = {GraphConfig.Value.TenantId}, ClientId = {GraphConfig.Value.ClientId}");

        var app = ConfidentialClientApplicationBuilder.Create(GraphConfig.Value.ClientId)
            .WithClientSecret(GraphConfig.Value.ClientSecret)
            .WithAuthority(AzureCloudInstance.AzurePublic, GraphConfig.Value.TenantId)
            .Build();

        var credentials = new ClientSecretCredential(
            GraphConfig.Value.TenantId,
            GraphConfig.Value.ClientId,
            GraphConfig.Value.ClientSecret,
            new TokenCredentialOptions { AuthorityHost = AzureAuthorityHosts.AzurePublicCloud });

        var graphClient = new GraphServiceClient(credentials);

        var graphMessage = new Message
        {
            From = CreateRecipient(FromUser(message, graphSettings.Value.FromEmailUser)),
            Subject = message.Subject,
            Body = new ItemBody
            {
                ContentType = BodyType.Html,
                Content = message.Body
            },

            ToRecipients = message.To.Where(e => !string.IsNullOrWhiteSpace(e)).Select(email => CreateRecipient(email!)).ToList(),
        };

        if (message.From != null)
        {
            graphMessage.From = CreateRecipient(message.From);
        }

        if (message.Cc != null && message.Cc.Length > 0)
        {
            graphMessage.CcRecipients = message.Cc.Select(a => CreateRecipient(a)).ToList();
        }

        if (message.Bcc != null && message.Bcc.Length > 0)
        {
            graphMessage.BccRecipients = message.Bcc.Select(a => CreateRecipient(a)).ToList();
        }

        if (message.ReplyTo != null && message.ReplyTo.Length > 0)
        {
            graphMessage.ReplyTo = message.ReplyTo.Select(a => CreateRecipient(a)).ToList();
        }

        graphMessage.Attachments = new List<Microsoft.Graph.Models.Attachment>();
        foreach (EmailMessageAttachment attachment in message.Attachments!)
        {
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                attachment.Stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            string base64 = Convert.ToBase64String(bytes);

            graphMessage.Attachments.Add(new Microsoft.Graph.Models.Attachment()
            {
                OdataType = "#microsoft.graph.fileAttachment",
                Name = attachment.FileName,
                ContentType = "text/plain",
                AdditionalData = new Dictionary<string, object>
                {
                    {
                        "contentBytes" , base64
                    }
                },
            });
        }

        try
        {
            await graphClient.Users[graphMessage.From.EmailAddress?.Address].SendMail.PostAsync(new Microsoft.Graph.Users.Item.SendMail.SendMailPostRequestBody { Message = graphMessage });
        }
        catch (Exception)
        {
            throw;
        }
        logger.LogInformation($"Email \'{message.Subject}\' to {message.To.ToString()} was successful via Graph");
    }

    private Recipient CreateRecipient(string email)
    {
        var recipient = new Recipient()
        {
            EmailAddress = new EmailAddress { Address = email }
        };

        if (MailboxAddress.TryParse(email, out InternetAddress internetAddress))
        {
            recipient.EmailAddress.Name = internetAddress.Name;
            if (internetAddress is MailboxAddress)
            {
                recipient.EmailAddress.Address = ((MailboxAddress)internetAddress).Address;
            }
        }
        return recipient;
    }

    private async Task SendAsyncInternalSmtp(EmailMessage message)
    {
        logger.LogInformation($"Trying to send \'{message.Subject}\' email to {message.To.ToString()} using Smtp");

        if (!globalSettings.Value.IsSmtpServerConfigured && !globalSettings.Value.IsPickupDirectoryLocationConfigured)
        {
            logger.LogDebug(
                "Could not send email for {Subject}. It was not handled by a notification handler and there is no SMTP configured.",
                message.Subject);
            return;
        }

        if (globalSettings.Value.IsPickupDirectoryLocationConfigured &&
            !string.IsNullOrWhiteSpace(globalSettings.Value.Smtp?.From))
        {
            // The following code snippet is the recommended way to handle PickupDirectoryLocation.
            // See more https://github.com/jstedfast/MailKit/blob/master/FAQ.md#q-how-can-i-send-email-to-a-specifiedpickupdirectory
            do
            {
                var path = Path.Combine(globalSettings.Value.Smtp.PickupDirectoryLocation!, Guid.NewGuid() + ".eml");
                Stream stream;

                try
                {
                    stream = System.IO.File.Open(path, FileMode.CreateNew);
                }
                catch (IOException)
                {
                    if (System.IO.File.Exists(path))
                    {
                        continue;
                    }

                    throw;
                }

                try
                {
                    using (stream)
                    {
                        using var filtered = new FilteredStream(stream);
                        filtered.Add(new SmtpDataFilter());

                        FormatOptions options = FormatOptions.Default.Clone();
                        options.NewLineFormat = NewLineFormat.Dos;

                        await message.ToMimeMessage(globalSettings.Value.Smtp.From).WriteToAsync(options, filtered);
                        filtered.Flush();
                        return;
                    }
                }
                catch
                {
                    System.IO.File.Delete(path);
                    throw;
                }
            }
            while (true);
        }

        using var client = new MailKit.Net.Smtp.SmtpClient();

        await client.ConnectAsync(
            globalSettings.Value.Smtp!.Host,
            globalSettings.Value.Smtp.Port,
            (MailKit.Security.SecureSocketOptions)(int)globalSettings.Value.Smtp.SecureSocketOptions);

        if (!string.IsNullOrWhiteSpace(globalSettings.Value.Smtp.Username) &&
            !string.IsNullOrWhiteSpace(globalSettings.Value.Smtp.Password))
        {
            await client.AuthenticateAsync(globalSettings.Value.Smtp.Username, globalSettings.Value.Smtp.Password);
        }

        var mailMessage = message.ToMimeMessage(FromUser(message, globalSettings.Value.Smtp.From));

        if (globalSettings.Value.Smtp.DeliveryMethod == SmtpDeliveryMethod.Network)
        {
            await client.SendAsync(mailMessage);
        }
        else
        {
            client.Send(mailMessage);
        }

        await client.DisconnectAsync(true);

        logger.LogInformation($"Email \'{message.Subject}\' to {message.To.ToString()} was successful via Smtp");

    }
}
