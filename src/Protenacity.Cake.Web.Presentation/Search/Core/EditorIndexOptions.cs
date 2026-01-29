using Examine;
using Examine.Lucene;
using Protenacity.Cake.Web.Core.Extensions;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Configuration.Models;

namespace Protenacity.Cake.Web.Presentation.Search.Core;


// Becareful: Adding other services to this class via DI can make Umbraco hang on boot up
public class EditorIndexOptions(IOptions<IndexCreatorSettings> settings) 
    : IConfigureNamedOptions<LuceneDirectoryIndexOptions>
{
    private IEnumerable<FieldDefinition> DefineFields()
    {
        var fieldDefs = new List<FieldDefinition>();

        fieldDefs.Add(new FieldDefinition(typeof(EditorSearchResult).Description(nameof(EditorSearchResult.DomainId)), FieldDefinitionTypes.FullText));
        fieldDefs.Add(new FieldDefinition(typeof(EditorSearchResult).Description(nameof(EditorSearchResult.ContentId)), FieldDefinitionTypes.FullText));
        fieldDefs.Add(new FieldDefinition(typeof(EditorSearchResult).Description(nameof(EditorSearchResult.Path)), FieldDefinitionTypes.FullText));
        foreach (var header in typeof(EditorSearchResult).Description(nameof(EditorSearchResult.Headers)).Split(','))
        {
            fieldDefs.Add(new FieldDefinition(header, FieldDefinitionTypes.FullText));
        }
        fieldDefs.Add(new FieldDefinition(typeof(EditorSearchResult).Description(nameof(EditorSearchResult.Body)), FieldDefinitionTypes.FullText));
        fieldDefs.Add(new FieldDefinition(typeof(EditorSearchResult).Description(nameof(EditorSearchResult.Keywords)), FieldDefinitionTypes.FullText));
        fieldDefs.Add(new FieldDefinition(typeof(EditorSearchResult).Description(nameof(EditorSearchResult.Promoted)), FieldDefinitionTypes.FullText));
        fieldDefs.Add(new FieldDefinition(typeof(EditorSearchResult).Description(nameof(EditorSearchResult.Priority)), FieldDefinitionTypes.FullText));
        fieldDefs.Add(new FieldDefinition(typeof(EditorSearchResult).Description(nameof(EditorSearchResult.Categories)), FieldDefinitionTypes.FullText));
        fieldDefs.Add(new FieldDefinition(typeof(EditorSearchResult).Description(nameof(EditorSearchResult.Abstract)), FieldDefinitionTypes.FullText));
        return fieldDefs;
    }

    public void Configure(string? name, LuceneDirectoryIndexOptions options)
    {
        if (name?.Equals(nameof(EditorIndex)) is false)
        {
            return;
        }

        options.Analyzer = new StandardAnalyzer(EditorIndex.Version);
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