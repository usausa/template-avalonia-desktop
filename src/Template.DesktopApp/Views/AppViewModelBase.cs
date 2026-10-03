namespace Template.DesktopApp.Views;

[ObservableGeneratorOption(Reactive = true, ViewModel = true)]
public abstract class AppViewModelBase :
    ExtendViewModelBase,
    INavigatorAware,
    INavigationEventSupport,
    INavigationLifecycleSupport
{
    public INavigator Navigator { get; set; } = default!;

    protected AppViewModelBase()
    {
        AcceptsCommand = false;
    }

    public virtual void OnNavigatingFrom(INavigationContext context)
    {
    }

    public virtual void OnNavigatingTo(INavigationContext context)
    {
    }

    public virtual void OnNavigatedTo(INavigationContext context)
    {
    }

    public void OnActivated() => AcceptsCommand = true;

    public void OnDeactivated() => AcceptsCommand = false;
}
