namespace Template.DesktopApp.Converters;

using Avalonia;
using Avalonia.Data.Converters;

public sealed class ResourceKeyConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        (value is not null) && (Application.Current is { } application) && application.TryGetResource(value, null, out var resource) ? resource : null;

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}
