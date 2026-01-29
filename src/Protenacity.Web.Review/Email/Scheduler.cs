using Microsoft.Extensions.Logging;
using Protenacity.Cake.Web.Core.Constitution;
using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Web.Review.Search.Internal;
using System.Globalization;
using System.Text.Json;
using Umbraco.Cms.Core.Mail;
using Umbraco.Cms.Core.Models.Email;
using Umbraco.Cms.Core.Models.Membership;
using Umbraco.Cms.Core.Persistence.Repositories;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Sync;
using Umbraco.Cms.Infrastructure.BackgroundJobs;

namespace Protenacity.Web.Review.Email;

public class Scheduler(IContentService contentService,
        ICoreScopeProvider scopeProvider,
        IKeyValueRepository keyValueRepository,
        IUserService userService,
        IRuntimeState runtimeState,
        ILogger<Scheduler> logger,
        IEmailSender emailSender,
        IReviewSearchInternalService reviewSearchInternalService)
    : IRecurringBackgroundJob
{
    public const int ContentPageSize = 20;  //  How many content records do we read per page
    public const int DatabaseDelay = 1000;  //  Delay in milliseconds between paged content results

    public TimeSpan Period => TimeSpan.FromHours(6);

    public TimeSpan Delay => TimeSpan.FromMinutes(1); // TimeSpan.FromMinutes(180 + Random.Shared.Next(0, 120));   // Add 2 hours worth of randomness so multiple servers don't clash

    public ServerRole[] ServerRoles = { ServerRole.Unknown, ServerRole.Single, ServerRole.SchedulingPublisher };

    public event EventHandler PeriodChanged { add { } remove { } }

    public class Domain
    {
        public int Id { get; set; }
        public required string From { get; set; }
        public required string Subject { get; set; }
        public required string Body { get; set; }
    }

    public Task RunJobAsync()
    {
        if (runtimeState.Level != Umbraco.Cms.Core.RuntimeLevel.Run)
        {
            return Task.CompletedTask;
        }

        var today = DateTime.UtcNow;
        var userToGroups = new Dictionary<int, IEnumerable<int>>();
        var users = new Dictionary<int, IUser>();
        foreach (var domain in Domains(today)) 
        {
            if (!keyValueRepository.TrySetKeyValue(scopeProvider, nameof(Review) + nameof(Email) + domain.Id.ToString() + ":" + today.ToString("yyyyMMMdd"), null, today.ToLongDateString()))
            {
                continue;
            }

            foreach (var userId in FindOverdueReviewDates(domain, today))
            {
                if (!userToGroups.ContainsKey(userId))
                {
                    userToGroups.Add(userId, userService.GetUserById(userId)?.Groups?.Select(g => g.Id) ?? Enumerable.Empty<int>());
                }
            }
            Thread.Sleep(DatabaseDelay);     //   We do this so we don't hammer the database

            var groups = userToGroups.SelectMany(u => u.Value).GroupBy(g => g).Select(g => g.Key);

            foreach (var group in groups)
            {
                foreach (var user in userService.GetAllInGroup(group))
                {
                    if (!users.ContainsKey(user.Id))
                    {
                        users.Add(user.Id, user);

                        EmailUser(domain, user);
                    }
                }
            }
            Thread.Sleep(DatabaseDelay);     //   We do this so we don't hammer the database
        }
        return Task.CompletedTask;
    }

    private IEnumerable<Domain> Domains(DateTime now)
    {
        var domains = new List<Domain>();
        var dayOfWeekText = now.ToString("dddd", CultureInfo.CreateSpecificCulture("en-GB"));

        foreach (var domainPage in contentService.GetRootContent().Where(c => c.ContentType.Alias == DomainPage.ModelTypeAlias && c.Published))
        {
            var frequencyJson = domainPage.GetValue<string>(typeof(DomainPage).ModelsBuilderAlias(nameof(DomainPage.ConfigReviewEmailFrequency)));
            if (string.IsNullOrWhiteSpace(frequencyJson))
            {
                continue;
            }

            var frequency = JsonSerializer.Deserialize<IEnumerable<string>>(frequencyJson);
            if (frequency?.Any(f => string.Compare(f, dayOfWeekText, true) == 0) == false)
            {
                continue;
            }

            domains.Add(new Domain
            {
                Id = domainPage.Id,
                From = domainPage.GetValue<string>(typeof(DomainPage).ModelsBuilderAlias(nameof(DomainPage.ConfigReviewEmailFrom))) ?? "ERROR: Subject missing",
                Subject = domainPage.GetValue<string>(typeof(DomainPage).ModelsBuilderAlias(nameof(DomainPage.ConfigReviewEmailSubject))) ?? "ERROR: Subject missing",
                Body = domainPage.GetValue<string>(typeof(DomainPage).ModelsBuilderAlias(nameof(DomainPage.ConfigReviewEmailBody))) ?? "ERROR: Body missing",
            });
        }
        return domains;
    }

    private IEnumerable<int> FindOverdueReviewDates(Domain domain, DateTime now)
    {
        var userIds = new List<int>();
        int page = 0;
        do
        {
            var results = reviewSearchInternalService.Search(domain.Id, null, null, page++, 100);
            if (results.Results.Any() != true)
            {
                return userIds;
            }
            foreach (var result in results.Results)
            {
                if (result.ReviewDate > now)
                {
                    return userIds;
                }
                foreach (var userGroup in result.UserGroups)
                {
                    if (!userIds.Contains(userGroup))
                    {
                        userIds.Add(userGroup);
                    }
                }
            }
        }
        while (true);
    }

    private bool EmailUser(Domain domain, IUser user)
    {
        logger.LogInformation("Send review email to " + user.Email);

        Task.Factory.StartNew(() => emailSender.SendAsync(new EmailMessage(domain.From, user.Email, domain.Subject, domain.Body, false), "Review"));

        return true;
    }
}
