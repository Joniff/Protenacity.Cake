using Protenacity.Cake.Web.Core.Extensions;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;

namespace Protenacity.Cake.Web.Presentation.Boot;

internal abstract class NotificationBase<TNotification> : INotificationHandler<TNotification>
    where TNotification : INotification
{
    protected void SetAlias(IContent content, Type publishedModel, string propertyName, object defaultValue, object? invalidValue = null)
    {
        var alias = publishedModel.ModelsBuilderAlias(propertyName);
        var value = content.GetValue(alias);

        if (value != null)
        {
            switch (value)
            {
                case int intValue:
                    if (invalidValue == null || intValue != (int)invalidValue)
                    {
                        return;
                    }
                    break;

                case string stringValue:
                    if (!string.IsNullOrWhiteSpace(stringValue) && (invalidValue == null || string.Compare(stringValue, (string)invalidValue, true) != 0))
                    {
                        return;
                    }
                    break;

                default:
                    throw new ApplicationException("Can only handle Int and String currently");
            }
        }

        content.SetValue(alias, defaultValue);
    }

    public virtual void Handle(TNotification notification)
    {
        throw new InvalidOperationException();
    }
}
