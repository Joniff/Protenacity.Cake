using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Api.Common.Attributes;
using Umbraco.Cms.Api.Management.Controllers;
using Umbraco.Cms.Web.Common.Authorization;
using Umbraco.Cms.Web.Common.Routing;

namespace Protenacity.Web.Review.Server;

[ApiController]
[BackOfficeRoute("review/api/v{version:apiVersion}")]
[Authorize(Policy = AuthorizationPolicies.BackOfficeAccess)]
[MapToApi(ReviewApiControllerBase.ApiName)]
public class ReviewApiControllerBase : ManagementApiControllerBase
{
    public const string ApiName = "review";
}
