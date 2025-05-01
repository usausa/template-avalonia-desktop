namespace Template.DesktopApp.Views.Main;

public sealed class MenuViewModel : AppViewModelBase
{
    public string Message { get; set; }

    public ICommand NavigateCommand { get; }

    public MenuViewModel()
    {
        Message = "Hello from MenuViewModel!";
        NavigateCommand = MakeDelegateCommand(() =>
        {
            Navigator.Forward(ViewId.Sub);
        });
    }
}
