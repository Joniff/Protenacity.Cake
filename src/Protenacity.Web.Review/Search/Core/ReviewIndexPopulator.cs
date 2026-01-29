using Examine;
using Microsoft.Extensions.Logging;
using Protenacity.Web.Review.Search.BackgroundTask;
using Umbraco.Cms.Infrastructure.Examine;

namespace Protenacity.Web.Review.Search.Core;

public class ReviewIndexPopulator : IndexPopulator
{
    private readonly IReviewSearchBackgroundTask reviewSearchBackgroundTask;
    private readonly ILogger<ReviewIndexPopulator> logger;

    public ReviewIndexPopulator(IReviewSearchBackgroundTask _editorSearchBackgroundTask,
        ILogger<ReviewIndexPopulator> _logger)
    {
        reviewSearchBackgroundTask = _editorSearchBackgroundTask;
        logger = _logger;
        RegisterIndex(nameof(ReviewIndex));
    }

    protected override void PopulateIndexes(IReadOnlyList<IIndex> indexes)
    {
        try
        {
            if (indexes.Any(i => i.Name.Equals(nameof(ReviewIndex))))
            {
                reviewSearchBackgroundTask.ExectionOfAllContentNow();
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "When trying to " + nameof(PopulateIndexes));
        }
    }
}
