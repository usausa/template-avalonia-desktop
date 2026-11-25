namespace Template.DesktopApp.Shell;

using Template.DesktopApp.Views;

public sealed class NavigationMenu
{
    public IReadOnlyList<NavigationNode> Items { get; } =
    [
        new NavigationItem(ViewId.Home, "Home", "HomeIcon"),
        new NavigationGroup(
            "Tasks",
            "TasksIcon",
            [
                new(ViewId.TaskList, "List", "ListIcon"),
                new(ViewId.TaskNew, "New", "AddIcon")
            ])
        {
            IsExpanded = true
        },
        new NavigationGroup(
            "Samples",
            "SamplesIcon",
            [
                new(ViewId.Dialogs, "Dialogs", "DialogsIcon"),
                new(ViewId.Navigation, "Navigation", "NavigationIcon")
            ])
        {
            IsExpanded = true
        }
    ];

    public IReadOnlyList<NavigationItem> FooterItems { get; } =
    [
        new(ViewId.Settings, "Settings", "SettingsIcon")
    ];

    public IReadOnlyList<NavigationItem> AllItems { get; }

    public NavigationMenu()
    {
        AllItems = EnumerateItems().ToArray();
    }

    public NavigationItem? Find(ViewId id)
    {
        foreach (var item in AllItems)
        {
            if (item.Id == id)
            {
                return item;
            }
        }

        return null;
    }

    public bool IsFooter(NavigationItem item) => FooterItems.Contains(item);

    public void Expand(NavigationItem item)
    {
        foreach (var node in Items)
        {
            if ((node is NavigationGroup group) && group.Items.Contains(item))
            {
                group.IsExpanded = true;
            }
        }
    }

    private IEnumerable<NavigationItem> EnumerateItems()
    {
        foreach (var node in Items)
        {
            if (node is NavigationGroup group)
            {
                foreach (var child in group.Items)
                {
                    yield return child;
                }
            }
            else if (node is NavigationItem item)
            {
                yield return item;
            }
        }

        foreach (var item in FooterItems)
        {
            yield return item;
        }
    }
}
