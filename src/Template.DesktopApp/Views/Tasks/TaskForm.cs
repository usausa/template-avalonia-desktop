namespace Template.DesktopApp.Views.Tasks;

using Template.DesktopApp.Models;

public sealed partial class TaskForm : ObservableObject, IDisposable
{
    public ErrorInfo Errors { get; } = new();

    [ObservableProperty]
    public partial string Name { get; set; } = string.Empty;

    [ObservableProperty]
    public partial Member? Assignee { get; set; }

    [ObservableProperty]
    public partial TaskState State { get; set; }

    [ObservableProperty]
    public partial TaskPriority Priority { get; set; }

    [ObservableProperty]
    public partial DateTime? DueDate { get; set; }

    [ObservableProperty]
    public partial double Estimate { get; set; }

    [ObservableProperty]
    public partial bool NotifyWatchers { get; set; }

    public TaskForm()
    {
        PropertyChanged += OnPropertyChanged;
    }

    public void Dispose()
    {
        PropertyChanged -= OnPropertyChanged;
        Errors.Dispose();
    }

    public void Load(TaskItem task)
    {
        Name = task.Name;
        Assignee = task.Assignee;
        State = task.State;
        Priority = task.Priority;
        DueDate = task.DueDate;
        Estimate = task.Estimate;
        NotifyWatchers = task.NotifyWatchers;
        Errors.ClearAllErrors();
    }

    public bool Validate()
    {
        Errors.ClearAllErrors();
        if (String.IsNullOrWhiteSpace(Name))
        {
            Errors.AddError(nameof(Name), "Enter a task name.");
        }
        if (Assignee is null)
        {
            Errors.AddError(nameof(Assignee), "Select an assignee.");
        }
        if (DueDate is null)
        {
            Errors.AddError(nameof(DueDate), "Select a due date.");
        }
        return !Errors.HasError;
    }

    public TaskItem ToItem(int id, int progress) =>
        new()
        {
            Id = id,
            Name = Name.Trim(),
            Assignee = Assignee!,
            State = State,
            Priority = Priority,
            DueDate = DueDate!.Value,
            Progress = progress,
            Estimate = Estimate,
            NotifyWatchers = NotifyWatchers
        };

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if ((e.PropertyName is { } name) && Errors.Contains(name))
        {
            Errors.ClearErrors(name);
        }
    }
}
