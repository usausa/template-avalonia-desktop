namespace Template.DesktopApp;

using Template.DesktopApp.Shell;
using Template.DesktopApp.Views;

// ReSharper disable once ClassNeverInstantiated.Global
[ObservableGeneratorOption(Reactive = true, ViewModel = true)]
public sealed partial class MainWindowViewModel : ExtendViewModelBase
{
    private BusyStateScope? navigatingBusy;

    public INavigator Navigator { get; }

    public NavigationMenu Menu { get; }

    [ObservableProperty]
    public partial NavigationNode? SelectedNode { get; set; }

    [ObservableProperty]
    public partial NavigationNode? SelectedFooterNode { get; set; }

    [ObservableProperty]
    public partial NavigationItem? SearchItem { get; set; }

    [ObservableProperty]
    public partial string? SearchText { get; set; }

    [ObservableProperty]
    public partial bool IsPaneOpen { get; set; } = true;

    public IObserveCommand TogglePaneCommand { get; }

    public MainWindowViewModel(INavigator navigator, NavigationMenu menu)
    {
        Navigator = navigator;
        Menu = menu;

        TogglePaneCommand = MakeDelegateCommand(() => IsPaneOpen = !IsPaneOpen);

        // Busy while navigating
        Disposables.Add(Observable.FromEventPattern<EventArgs>(h => Navigator.ExecutingChanged += h, h => Navigator.ExecutingChanged -= h)
            .Subscribe(_ => UpdateNavigatingBusy()));

        // Menu
        Disposables.Add(Observable.FromEventPattern<NavigationEventArgs>(h => Navigator.Navigated += h, h => Navigator.Navigated -= h)
            .Subscribe(x => OnNavigated(x.EventArgs.Context.ToId as ViewId?)));
        Disposables.Add(ObserveSelectedNode()
            .Merge(ObserveSelectedFooterNode())
            .OfType<NavigationItem>()
            .Select(x => Observable.FromAsync(() => ForwardAsync(x.Id)))
            .Concat()
            .Subscribe());

        // Search
        Disposables.Add(ObserveSearchItem()
            .OfType<NavigationItem>()
            .Select(x => Observable.FromAsync(() => SearchAsync(x.Id)))
            .Concat()
            .Subscribe());
    }

    private void UpdateNavigatingBusy()
    {
        if (Navigator.Executing)
        {
            navigatingBusy ??= BusyState.Begin();
        }
        else
        {
            navigatingBusy?.Dispose();
            navigatingBusy = null;
        }
    }

    private void OnNavigated(ViewId? id)
    {
        var item = id is { } value ? Menu.Find(value) : null;
        if (item is null)
        {
            SelectedNode = null;
            SelectedFooterNode = null;
        }
        else if (Menu.IsFooter(item))
        {
            SelectedNode = null;
            SelectedFooterNode = item;
        }
        else
        {
            Menu.Expand(item);
            SelectedFooterNode = null;
            SelectedNode = item;
        }
    }

    private Task ForwardAsync(ViewId id) =>
        Navigator.Executing || Equals(Navigator.CurrentViewId, id) ? Task.CompletedTask : Navigator.ForwardAsync(id);

    private async Task SearchAsync(ViewId id)
    {
        await ForwardAsync(id);
        SearchText = string.Empty;
        SearchItem = null;
    }
}
