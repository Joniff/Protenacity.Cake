using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Routing;

namespace LCC.RollbackPreview.Server;

public class UmbracoCommunityRollbackPreviewerApiComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.ContentFinders().InsertBefore<ContentFinderByUrlNew, RollBackPreviewContentFinder>();
        builder.Services.AddTransient<PublishedContentConverter>();
    }
}
