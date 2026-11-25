namespace Template.DesktopApp.Models;

public sealed record TaskItem
{
    public int Id { get; init; }

    public required string Name { get; init; }

    public required Member Assignee { get; init; }

    public TaskState State { get; init; }

    public TaskPriority Priority { get; init; }

    public DateTime DueDate { get; init; }

    public int Progress { get; init; }

    public double Estimate { get; init; }

    public bool NotifyWatchers { get; init; }
}
