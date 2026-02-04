using Protenacity.Cake.Web.Core.Extensions;
using Protenacity.Cake.Web.Core.Property;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Protenacity.Cake.Web.Core.Constitution;

public partial interface IAlertTab
{
    AlertStatuses AlertStatusTyped { get; }
    AlertTypes AlertTypeTyped { get; }
}

public partial class AlertTab
{
    public AlertStatuses AlertStatusTyped => AlertStatuses.ParseByDescription(this.AlertStatus);
    public AlertTypes AlertTypeTyped => AlertTypes.ParseByDescription(this.AlertType);
}
