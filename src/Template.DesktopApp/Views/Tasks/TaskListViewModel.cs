namespace Template.DesktopApp.Views.Tasks;

using Template.DesktopApp.Models;
using Template.DesktopApp.Services;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed partial class TaskListViewModel : AppViewModelBase
{
    private readonly TaskService taskService;

    public ObservableCollection<TaskItem> Tasks { get; } = [];

    public IReadOnlyList<Member> Members => taskService.Members;

    public TaskForm Form { get; } = new();

    [ObservableProperty]
    public partial string FilterText { get; set; } = string.Empty;

    [ObservableProperty]
    public partial bool ShowCompleted { get; set; } = true;

    [ObservableProperty]
    public partial TaskSort Sort { get; set; }

    [ObservableProperty]
    public partial TaskItem? SelectedTask { get; set; }

    [ObservableProperty]
    public partial int OpenCount { get; set; }

    public IObserveCommand NewCommand { get; }

    public IObserveCommand SaveCommand { get; }

    public IObserveCommand CancelCommand { get; }

    public TaskListViewModel(TaskService taskService)
    {
        this.taskService = taskService;
        Disposables.Add(Form);

        NewCommand = MakeAsyncCommand(() => Navigator.ForwardAsync(ViewId.TaskNew));
        SaveCommand = MakeDelegateCommand(Save);
        CancelCommand = MakeDelegateCommand(Cancel);

        SubscribeFilterText(_ => Refresh());
        SubscribeShowCompleted(_ => Refresh());
        SubscribeSort(_ => Refresh());
        SubscribeSelectedTask(x =>
        {
            if (x is not null)
            {
                Form.Load(x);
            }
        });

        Refresh();
        SelectedTask = Tasks.FirstOrDefault();
    }

    private void Refresh()
    {
        var selectedId = SelectedTask?.Id;
        var tasks = taskService.QueryTaskList();
        OpenCount = tasks.Count(static x => x.State != TaskState.Done);

        var query = tasks.AsEnumerable();
        if (!ShowCompleted)
        {
            query = query.Where(static x => x.State != TaskState.Done);
        }
        if (!String.IsNullOrWhiteSpace(FilterText))
        {
            var text = FilterText.Trim();
            query = query.Where(x => x.Name.Contains(text, StringComparison.CurrentCultureIgnoreCase));
        }
        query = Sort switch
        {
            TaskSort.Name => query.OrderBy(static x => x.Name, StringComparer.CurrentCulture),
            TaskSort.Progress => query.OrderByDescending(static x => x.Progress),
            _ => query.OrderBy(static x => x.DueDate)
        };

        Tasks.Clear();
        foreach (var task in query)
        {
            Tasks.Add(task);
        }

        SelectedTask = selectedId is { } id ? Tasks.FirstOrDefault(x => x.Id == id) : null;
    }

    private void Save()
    {
        if ((SelectedTask is not { } task) || !Form.Validate())
        {
            return;
        }

        taskService.UpdateTask(Form.ToItem(task.Id, task.Progress));
        Refresh();
    }

    private void Cancel()
    {
        if (SelectedTask is { } task)
        {
            Form.Load(task);
        }
    }
}
