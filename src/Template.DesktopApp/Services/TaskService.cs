namespace Template.DesktopApp.Services;

using Template.DesktopApp.Models;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class TaskService
{
    private readonly List<TaskItem> tasks;

    private int nextId;

    public IReadOnlyList<Member> Members { get; }

    public TaskService(TimeProvider timeProvider)
    {
        var today = timeProvider.GetLocalNow().Date;

        var ken = new Member("Ken", "#2E7D32");
        var sara = new Member("Sara", "#C2551F");
        var nora = new Member("Nora", "#B83BB0");
        var joe = new Member("Joe", "#6A3DB8");
        var emma = new Member("Emma", "#2F6FD0");
        Members = [ken, sara, nora, joe, emma];

        tasks =
        [
            Create(1, "Finalize hero illustrations", ken, TaskState.InReview, TaskPriority.High, -1, 90, 8),
            Create(2, "Responsive navigation menu", sara, TaskState.InProgress, TaskPriority.High, 1, 55, 16),
            Create(3, "Write copy for the pricing page", nora, TaskState.InProgress, TaskPriority.Medium, 3, 35, 6),
            Create(4, "Set up analytics events", joe, TaskState.Backlog, TaskPriority.Low, 10, 0, 4),
            Create(5, "Accessibility audit", emma, TaskState.InReview, TaskPriority.High, 0, 80, 12),
            Create(6, "Migrate blog to new CMS", sara, TaskState.Done, TaskPriority.Medium, -4, 100, 20),
            Create(7, "Optimize image loading", joe, TaskState.InProgress, TaskPriority.Medium, 4, 45, 8),
            Create(8, "Customer testimonials carousel", ken, TaskState.Backlog, TaskPriority.Low, 12, 5, 6),
            Create(9, "Newsletter signup form", nora, TaskState.Done, TaskPriority.Low, -6, 100, 4),
            Create(10, "Cross-browser QA pass", emma, TaskState.Backlog, TaskPriority.Medium, 14, 0, 10),
            Create(11, "Update privacy policy page", ken, TaskState.InProgress, TaskPriority.Medium, -2, 20, 3),
            Create(12, "Performance budget review", joe, TaskState.Backlog, TaskPriority.Low, 19, 0, 2)
        ];
        nextId = tasks.Count + 1;

        TaskItem Create(int id, string name, Member assignee, TaskState state, TaskPriority priority, int dueDays, int progress, double estimate) =>
            new()
            {
                Id = id,
                Name = name,
                Assignee = assignee,
                State = state,
                Priority = priority,
                DueDate = today.AddDays(dueDays),
                Progress = progress,
                Estimate = estimate,
                NotifyWatchers = priority == TaskPriority.High
            };
    }

    public IReadOnlyList<TaskItem> QueryTaskList() => tasks.ToArray();

    public void UpdateTask(TaskItem task)
    {
        var index = tasks.FindIndex(x => x.Id == task.Id);
        if (index >= 0)
        {
            tasks[index] = task;
        }
    }

    public TaskItem InsertTask(TaskItem task)
    {
        var item = task with { Id = nextId++ };
        tasks.Add(item);
        return item;
    }
}
