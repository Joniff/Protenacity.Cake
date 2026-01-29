using Examine;
using Examine.Lucene;
using Protenacity.Cake.Web.Core.Extensions;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Configuration.Models;

namespace Protenacity.Web.Review.Search.Core;


// Becareful: Adding other services to this class via DI can make Umbraco hang on boot up
public class ReviewIndexOptions(IOptions<IndexCreatorSettings> settings) 
    : IConfigureNamedOptions<LuceneDirectoryIndexOptions>
{
    private IEnumerable<FieldDefinition> DefineFields()
    {
        var fieldDefs = new List<FieldDefinition>();

        fieldDefs.Add(new FieldDefinition(typeof(ReviewSearchResult).Description(nameof(ReviewSearchResult.Key)), FieldDefinitionTypes.FullText));
        fieldDefs.Add(new FieldDefinition(typeof(ReviewSearchResult).Description(nameof(ReviewSearchResult.DomainId)), FieldDefinitionTypes.FullText));
        fieldDefs.Add(new FieldDefinition(typeof(ReviewSearchResult).Description(nameof(ReviewSearchResult.ContentId)), FieldDefinitionTypes.FullText));
        fieldDefs.Add(new FieldDefinition(typeof(ReviewSearchResult).Description(nameof(ReviewSearchResult.PathIds)), FieldDefinitionTypes.FullText));
        fieldDefs.Add(new FieldDefinition(typeof(ReviewSearchResult).Description(nameof(ReviewSearchResult.PathNames)), FieldDefinitionTypes.FullText));
        fieldDefs.Add(new FieldDefinition(typeof(ReviewSearchResult).Description(nameof(ReviewSearchResult.Name)), FieldDefinitionTypes.FullText));
        fieldDefs.Add(new FieldDefinition(typeof(ReviewSearchResult).Description(nameof(ReviewSearchResult.ReviewDate)), FieldDefinitionTypes.DateTime));
        fieldDefs.Add(new FieldDefinition(typeof(ReviewSearchResult).Description(nameof(ReviewSearchResult.UserGroups)), FieldDefinitionTypes.FullText));
        return fieldDefs;
    }

    public void Configure(string? name, LuceneDirectoryIndexOptions options)
    {
        if (name?.Equals(nameof(ReviewIndex)) is false)
        {
            return;
        }

        options.Analyzer = new StandardAnalyzer(ReviewIndex.Version);
        options.FieldDefinitions = new FieldDefinitionCollection(DefineFields().ToArray());
        options.UnlockIndex = true;

        if (settings.Value.LuceneDirectoryFactory == LuceneDirectoryFactory.SyncedTempFileSystemDirectoryFactory)
        {
            // if this directory factory is enabled then a snapshot deletion policy is required
            options.IndexDeletionPolicy = new SnapshotDeletionPolicy(new KeepOnlyLastCommitDeletionPolicy());
        }
    }

    // not used
    public void Configure(LuceneDirectoryIndexOptions options) => throw new NotImplementedException();
}