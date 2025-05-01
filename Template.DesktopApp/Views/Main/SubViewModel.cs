namespace Template.DesktopApp.Views.Main;

public sealed class SubViewModel : AppViewModelBase
{
    public string Message { get; set; }

    public ICommand NavigateCommand { get; }

    public SubViewModel()
    {
        Message = "Hello from SubViewModel!";
        NavigateCommand = MakeDelegateCommand(() =>
        {
            Navigator.Forward(ViewId.Menu);
        });
    }
}
