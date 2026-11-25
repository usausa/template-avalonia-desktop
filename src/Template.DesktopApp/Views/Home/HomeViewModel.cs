namespace Template.DesktopApp.Views.Home;

using Template.DesktopApp.Models;
using Template.DesktopApp.Services;
using Template.DesktopApp.Settings;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class HomeViewModel : AppViewModelBase
{
    private const int UpcomingCount = 5;

    public string Setting { get; }

    public int TotalCount { get; }

    public int OpenCount { get; }

    public int DoneCount { get; }

    public int OverdueCount { get; }

    public double PlannedHours { get; }

    public double DonePercent { get; }

    public IReadOnlyList<TaskItem> UpcomingTasks { get; }

    public IObserveCommand ListCommand { get; }

    public HomeViewModel(TimeProvider timeProvider, Setting setting, TaskService taskService)
    {
        var today = timeProvider.GetLocalNow().Date;
        var tasks = taskService.QueryTaskList();
        var openTasks = tasks.Where(static x => x.State != TaskState.Done).ToArray();

        Setting = setting.Value;
        TotalCount = tasks.Count;
        OpenCount = openTasks.Length;
        DoneCount = TotalCount - OpenCount;
        OverdueCount = openTasks.Count(x => x.DueDate < today);
        PlannedHours = openTasks.Sum(static x => x.Estimate);
        DonePercent = TotalCount > 0 ? DoneCount * 100d / TotalCount : 0;
        UpcomingTasks = openTasks.OrderBy(static x => x.DueDate).Take(UpcomingCount).ToArray();

        ListCommand = MakeAsyncCommand(() => Navigator.ForwardAsync(ViewId.TaskList));
    }
}
