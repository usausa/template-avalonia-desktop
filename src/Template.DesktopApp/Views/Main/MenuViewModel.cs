namespace Template.DesktopApp.Views.Main;

using Template.DesktopApp.Services;
using Template.DesktopApp.Settings;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed partial class MenuViewModel : AppViewModelBase
{
    [ObservableProperty]
    public partial string Message { get; set; }

    public IObserveCommand ForwardCommand { get; }

    public IObserveCommand ThemeCommand { get; }

    public MenuViewModel(Setting setting, ThemeService themeService)
    {
        Message = $"Hello from MenuViewModel! setting=[{setting.Value}]";
        ForwardCommand = MakeAsyncCommand<ViewId>(x => Navigator.ForwardAsync(x));
        ThemeCommand = MakeAsyncCommand<string>(x => themeService.ChangeAsync(x).AsTask());
    }
}
