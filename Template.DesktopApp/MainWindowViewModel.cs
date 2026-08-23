namespace Template.DesktopApp;

// ReSharper disable once ClassNeverInstantiated.Global
[ObservableGeneratorOption(Reactive = true, ViewModel = true)]
public sealed class MainWindowViewModel : ExtendViewModelBase
{
    public Navigator Navigator { get; }

    public MainWindowViewModel(Navigator navigator)
    {
        Navigator = navigator;
    }
}
