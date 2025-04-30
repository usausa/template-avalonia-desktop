namespace Template.DesktopApp;

using Template.DesktopApp.Modules;

public static partial class Components
{
    [ViewSource]
    public static partial IEnumerable<KeyValuePair<ViewId, Type>> ViewSource();
}
