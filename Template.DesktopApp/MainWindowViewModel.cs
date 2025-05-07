namespace Template.DesktopApp;

using Smart.Avalonia.ViewModels;

public class MainWindowViewModel : AvaloniaViewModelBase
{
    public Navigator Navigator { get; set; }

    public MainWindowViewModel(Navigator navigator)
    {
        Navigator = navigator;
    }
}
