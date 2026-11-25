namespace Template.DesktopApp.Views.Samples;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class NavigationSubViewModel : AppViewModelBase
{
    public IObserveCommand ForwardCommand { get; }

    public NavigationSubViewModel()
    {
        ForwardCommand = MakeAsyncCommand<ViewId>(x => Navigator.ForwardAsync(x));
    }
}
