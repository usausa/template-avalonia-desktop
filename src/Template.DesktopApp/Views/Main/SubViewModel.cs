namespace Template.DesktopApp.Views.Main;

using Template.DesktopApp.Services;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed partial class SubViewModel : AppViewModelBase
{
    [ObservableProperty]
    public partial string Message { get; set; }

    [ObservableProperty]
    public partial string Result { get; set; }

    public IObserveCommand ConfirmCommand { get; }

    public IObserveCommand InputCommand { get; }

    public IObserveCommand NavigateCommand { get; }

    public SubViewModel(IDialogService dialogService)
    {
        Message = "Hello from SubViewModel!";
        Result = string.Empty;
        ConfirmCommand = MakeAsyncCommand(async () =>
        {
            Result = await dialogService.ConfirmAsync("Are you sure?") ? "Confirmed" : "Canceled";
        });
        InputCommand = MakeAsyncCommand(async () =>
        {
            var value = await dialogService.InputAsync("Input value", Result);
            if (value is not null)
            {
                Result = value;
            }
        });
        NavigateCommand = MakeAsyncCommand(() => Navigator.ForwardAsync(ViewId.Menu));
    }
}
