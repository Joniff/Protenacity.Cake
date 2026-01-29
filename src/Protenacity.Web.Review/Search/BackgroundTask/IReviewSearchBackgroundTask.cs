using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Persistence.Repositories;
using Umbraco.Cms.Core.Scoping;

namespace Protenacity.Web.Review.Search.BackgroundTask;

public interface IReviewSearchBackgroundTask
{
    bool ScheduleExectionOfAllContent();
    bool ScheduleExection(int parentId);
    bool DeleteSchedule(int parentId, DateTime updateDate);
    IEnumerable<Tuple<int, DateTime>> GetSchedules();
    bool ExectionOfAllContentNow();
    bool ExecuteNow(int parentId);
}

