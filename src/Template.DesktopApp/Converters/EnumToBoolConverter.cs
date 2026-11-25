namespace Template.DesktopApp.Converters;

using Avalonia.Data;
using Avalonia.Data.Converters;

public sealed class EnumToBoolConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        Equals(value, parameter);

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value is true ? parameter : BindingOperations.DoNothing;
}
