using Examine;
using Microsoft.Extensions.DependencyInjection;
using Protenacity.Web.Review.Search.BackgroundTask;
using Protenacity.Web.Review.Search.Core;
using Protenacity.Web.Review.Search.Internal;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Infrastructure.Examine;
using Umbraco.Extensions;

namespace Protenacity.Web.Review.Boot;

public class BootComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        //builder.Services.AddRecurringBackgroundJob<Scheduler>();

        builder.Services.AddExamineLuceneIndex<ReviewIndex, ConfigurationEnabledDirectoryFactory>(nameof(ReviewIndex));
        builder.Services.ConfigureOptions<ReviewIndexOptions>();
        builder.Services.AddSingleton<IReviewSearchInternalService, ReviewSearchInternalService>();
        builder.Services.AddSingleton<ReviewIndexValueSetBuilder>();
        builder.Services.AddSingleton<IIndexPopulator, ReviewIndexPopulator>();
        builder.AddNotificationHandler<ContentCacheRefresherNotification, ReviewIndexingNotificationHandler>();
        builder.Services.AddSingleton<IReviewSearchBackgroundTask, ReviewSearchBackgroundTask>();
        builder.Services.AddRecurringBackgroundJob<ReviewSearchScheduler>();
    }
}
