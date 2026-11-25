namespace Template.DesktopApp.Views.Samples;

using Template.DesktopApp.Services;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed partial class DialogsViewModel : AppViewModelBase
{
    [ObservableProperty]
    public partial string Result { get; set; }

    public IObserveCommand ConfirmCommand { get; }

    public IObserveCommand InputCommand { get; }

    public IObserveCommand NoticeCommand { get; }

    public DialogsViewModel(IDialogService dialogService)
    {
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
        NoticeCommand = MakeAsyncCommand(async () =>
        {
            await dialogService.NotifyAsync("Hello from DialogsViewModel!");
            Result = "Noticed";
        });
    }
}
