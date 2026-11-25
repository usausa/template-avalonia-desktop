namespace Template.DesktopApp.Shell;

using Template.DesktopApp.Views;

public sealed class NavigationItem : NavigationNode
{
    public ViewId Id { get; }

    public NavigationItem(ViewId id, string title, string icon)
        : base(title, icon)
    {
        Id = id;
    }
}
