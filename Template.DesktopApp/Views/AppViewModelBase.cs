namespace Template.DesktopApp.Views;

using Smart.Avalonia.ViewModels;

public abstract class AppViewModelBase : AvaloniaViewModelBase, INavigatorAware, INavigationEventSupport
{
    public INavigator Navigator { get; set; } = default!;

    public void OnNavigatingFrom(INavigationContext context)
    {
    }

    public void OnNavigatingTo(INavigationContext context)
    {
    }

    public void OnNavigatedTo(INavigationContext context)
    {
    }
}
