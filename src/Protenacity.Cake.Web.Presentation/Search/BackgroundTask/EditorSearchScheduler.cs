using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.BackgroundJobs;

namespace Protenacity.Cake.Web.Presentation.Search.BackgroundTask;

public class EditorSearchScheduler(
    IRuntimeState runtimeState,
    IEditorSearchBackgroundTask editorSearchBackgroundTask)
    : IRecurringBackgroundJob
{
    public const int DatabaseDelay = 1000;  //  Delay in milliseconds between paged content results
    public TimeSpan Period => TimeSpan.FromMinutes(5);
    public TimeSpan Delay => TimeSpan.FromMinutes(5);

    public event EventHandler PeriodChanged { add { } remove { } }

    public Task RunJobAsync()
    {
        if (runtimeState.Level != RuntimeLevel.Run)
        {
            return Task.CompletedTask;
        }

        var schedules = editorSearchBackgroundTask.GetSchedules();

        if (schedules?.Any() != true)
        {
            return Task.CompletedTask;
        }

        foreach (var schedule in schedules)
        {
            if (schedules.Where(s => s.Item1 == schedule.Item1).First().Item2 == schedule.Item2)
            {
                if (!editorSearchBackgroundTask.ExecuteNow(schedule.Item1))
                {
                    return Task.CompletedTask;
                }
            }

            if (!editorSearchBackgroundTask.DeleteSchedule(schedule.Item1, schedule.Item2))
            {
                return Task.CompletedTask;
            }

            Thread.Sleep(DatabaseDelay);     //   We do this so we don't hammer the database
        }
        return Task.CompletedTask;
    }
}

