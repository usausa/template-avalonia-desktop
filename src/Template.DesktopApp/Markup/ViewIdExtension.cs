namespace Template.DesktopApp.Markup;

using Avalonia.Markup.Xaml;
using Avalonia.Metadata;

using Template.DesktopApp.Views;

public sealed class ViewIdExtension : MarkupExtension
{
    [Content]
    public ViewId Value { get; set; }

    public ViewIdExtension(ViewId value)
    {
        Value = value;
    }

    public override object ProvideValue(IServiceProvider serviceProvider) => Value;
}
