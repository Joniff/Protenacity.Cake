using Examine;
using Microsoft.Extensions.DependencyInjection;
using Protenacity.Cake.Web.Presentation.Category;
using Protenacity.Cake.Web.Presentation.Editor;
using Protenacity.Cake.Web.Presentation.Form;
using Protenacity.Cake.Web.Presentation.Pages.CategoriesData;
using Protenacity.Cake.Web.Presentation.Pages.Domain;
using Protenacity.Cake.Web.Presentation.Pages.Editor;
using Protenacity.Cake.Web.Presentation.Search;
using Protenacity.Cake.Web.Presentation.Search.BackgroundTask;
using Protenacity.Cake.Web.Presentation.Search.Core;
using Protenacity.Cake.Web.Presentation.Search.Internal;
using Protenacity.Cake.Web.Presentation.View;
using Protenacity.Spreadsheet;
using Sagara.FeedReader.Extensions;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Infrastructure.Examine;
using Umbraco.Extensions;

namespace Protenacity.Cake.Web.Presentation.Boot;

public class RegisterComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.AddSpreadsheet();
        builder.Services.AddScoped<IViewService, ViewService>();
        builder.Services.AddTransient<IResponsiveImageService, ResponsiveImageService>();
        builder.Services.AddTransient<IVideoService, VideoService>();
        builder.Services.AddTransient<IEditorService, EditorService>();
        builder.Services.AddTransient<IFormReaderService, FormReaderService>();
        builder.AddNotificationHandler<ContentSavingNotification, DomainPageNotifications>();
        builder.AddNotificationHandler<ContentSavingNotification, EditorPageNotifications>();
        builder.SetContentLastChanceFinder<PageNotFoundContentFinder>();

        builder.Services.AddExamineLuceneIndex<EditorIndex, ConfigurationEnabledDirectoryFactory>(nameof(EditorIndex));
        builder.Services.ConfigureOptions<EditorIndexOptions>();
        builder.Services.AddSingleton<IEditorSearchInternalService, EditorSearchInternalService>();
        builder.Services.AddSingleton<EditorIndexValueSetBuilder>();
        builder.Services.AddSingleton<IIndexPopulator, EditorIndexPopulator>();
        builder.AddNotificationHandler<ContentCacheRefresherNotification, EditorIndexingNotificationHandler>();
        builder.Services.AddSingleton<IEditorSearchService, EditorSearchService>();
        builder.Services.AddSingleton<IEditorSearchBackgroundTask, EditorSearchBackgroundTask>();
        builder.Services.AddRecurringBackgroundJob<EditorSearchScheduler>();
        builder.Services.AddFeedReaderServices();
        builder.Services.AddTransient<ICategoryService, CategoryService>();
        builder.AddNotificationHandler<ContentSavingNotification, CategoriesDataNotifications>();
        builder.AddNotificationHandler<ContentSavingNotification, CategoryHeaderDataNotifications>();
    }
}
