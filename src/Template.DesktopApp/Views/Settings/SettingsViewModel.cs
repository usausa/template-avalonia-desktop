namespace Template.DesktopApp.Views.Settings;

using System.Runtime.InteropServices;

using Template.DesktopApp.Services;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed partial class SettingsViewModel : AppViewModelBase
{
    public IReadOnlyList<string> Themes { get; } = ["Light", "Dark", "System"];

    [ObservableProperty]
    public partial string Theme { get; set; }

    public string Version { get; } = typeof(App).Assembly.GetName().Version?.ToString() ?? string.Empty;

    public string Framework { get; } = RuntimeInformation.FrameworkDescription;

    public string OperatingSystem { get; } = RuntimeInformation.OSDescription;

    public SettingsViewModel(ThemeService themeService)
    {
        Theme = themeService.Current;
        Disposables.Add(ObserveTheme()
            .Select(x => Observable.FromAsync(() => themeService.ChangeAsync(x).AsTask()))
            .Concat()
            .Subscribe());
    }
}
