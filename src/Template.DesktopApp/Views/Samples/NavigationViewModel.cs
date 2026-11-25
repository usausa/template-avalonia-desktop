namespace Template.DesktopApp.Views.Samples;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class NavigationViewModel : AppViewModelBase
{
    public IObserveCommand ForwardCommand { get; }

    public NavigationViewModel()
    {
        ForwardCommand = MakeAsyncCommand<ViewId>(x => Navigator.ForwardAsync(x));
    }
}
