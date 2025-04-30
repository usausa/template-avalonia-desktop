namespace Template.DesktopApp;

using CommunityToolkit.Mvvm.ComponentModel;

public class MainWindowViewModel : ObservableObject
{
    public Navigator Navigator { get; set; }

    public MainWindowViewModel(Navigator navigator)
    {
        Navigator = navigator;
    }
}
