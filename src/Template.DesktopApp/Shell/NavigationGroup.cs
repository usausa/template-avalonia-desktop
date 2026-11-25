namespace Template.DesktopApp.Shell;

public sealed partial class NavigationGroup : NavigationNode
{
    public IReadOnlyList<NavigationItem> Items { get; }

    [ObservableProperty]
    public partial bool IsExpanded { get; set; }

    public NavigationGroup(string title, string icon, IReadOnlyList<NavigationItem> items)
        : base(title, icon)
    {
        Items = items;
    }
}
