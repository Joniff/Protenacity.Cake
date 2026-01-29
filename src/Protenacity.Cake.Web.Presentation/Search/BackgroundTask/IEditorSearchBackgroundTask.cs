using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Persistence.Repositories;
using Umbraco.Cms.Core.Scoping;

namespace Protenacity.Cake.Web.Presentation.Search.BackgroundTask;

public interface IEditorSearchBackgroundTask
{
    bool ScheduleExectionOfAllContent();
    bool ScheduleExection(int parentId);
    bool DeleteSchedule(int parentId, DateTime updateDate);
    IEnumerable<Tuple<int, DateTime>> GetSchedules();
    bool ExectionOfAllContentNow();
    bool ExecuteNow(int parentId);
}
