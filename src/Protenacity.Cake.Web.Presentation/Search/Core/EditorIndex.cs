using Examine.Lucene;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.Examine;

namespace Protenacity.Cake.Web.Presentation.Search.Core;

public class EditorIndex(
    ILoggerFactory loggerFactory,
    string name,
    IOptionsMonitor<LuceneDirectoryIndexOptions> indexOptions,
    Umbraco.Cms.Core.Hosting.IHostingEnvironment hostingEnvironment,
    IRuntimeState runtimeState) 
    : UmbracoExamineIndex(loggerFactory,
        name,
        indexOptions,
        hostingEnvironment,
        runtimeState)
{
    public const Lucene.Net.Util.LuceneVersion Version = Lucene.Net.Util.LuceneVersion.LUCENE_48;

}