using OpenIddict.Abstractions;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace Protenacity.Cake.Web.Core.Mcp;

public class RegisterMcpClientHandler
    : INotificationAsyncHandler<UmbracoApplicationStartingNotification>
{
    private readonly IOpenIddictApplicationManager _applicationManager;

    public RegisterMcpClientHandler(IOpenIddictApplicationManager applicationManager)
    {
        _applicationManager = applicationManager;
    }

    public async Task HandleAsync(
        UmbracoApplicationStartingNotification notification,
        CancellationToken cancellationToken)
    {
        const string clientId = "umbraco-back-office-claude";

        // Remove any existing registration so we can update it cleanly
        var existing = await _applicationManager.FindByClientIdAsync(clientId, cancellationToken);
        if (existing is not null)
        {
            await _applicationManager.DeleteAsync(existing, cancellationToken);
        }

        var descriptor = new OpenIddictApplicationDescriptor
        {
            ClientId = clientId,
            ClientType = OpenIddictConstants.ClientTypes.Public,
            DisplayName = "Umbraco MCP Server",
            RedirectUris =
            {
                // Production callback URL
                new Uri("https://terratype.world/callback"),
                // Local development callback URL
                new Uri("https://localhost:44302/callback"),
            },
            // Required for "Log in as different user" (RP-Initiated Logout)
            PostLogoutRedirectUris =
            {
                new Uri("https://terratype.world/logout-callback"),
                new Uri("https://localhost:44302/logout-callback"),
            },
            Permissions =
            {
                OpenIddictConstants.Permissions.Endpoints.Authorization,
                OpenIddictConstants.Permissions.Endpoints.Token,
                OpenIddictConstants.Permissions.Endpoints.Revocation,
                OpenIddictConstants.Permissions.Endpoints.EndSession,
                OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                OpenIddictConstants.Permissions.ResponseTypes.Code,
            }
        };

        await _applicationManager.CreateAsync(descriptor, cancellationToken);
    }
}