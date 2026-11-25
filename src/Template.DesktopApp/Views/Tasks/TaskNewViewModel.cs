namespace Template.DesktopApp.Views.Tasks;

using Template.DesktopApp.Models;
using Template.DesktopApp.Services;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class TaskNewViewModel : AppViewModelBase
{
    private readonly TaskService taskService;

    public IReadOnlyList<Member> Members => taskService.Members;

    public TaskForm Form { get; } = new();

    public IObserveCommand SaveCommand { get; }

    public IObserveCommand CancelCommand { get; }

    public TaskNewViewModel(TimeProvider timeProvider, TaskService taskService)
    {
        this.taskService = taskService;
        Disposables.Add(Form);

        Form.State = TaskState.Backlog;
        Form.Priority = TaskPriority.Medium;
        Form.DueDate = timeProvider.GetLocalNow().Date.AddDays(7);
        Form.Estimate = 8;
        Form.NotifyWatchers = true;

        SaveCommand = MakeAsyncCommand(SaveAsync);
        CancelCommand = MakeAsyncCommand(() => Navigator.ForwardAsync(ViewId.TaskList));
    }

    private async Task SaveAsync()
    {
        if (!Form.Validate())
        {
            return;
        }

        taskService.InsertTask(Form.ToItem(0, 0));
        await Navigator.ForwardAsync(ViewId.TaskList);
    }
}
