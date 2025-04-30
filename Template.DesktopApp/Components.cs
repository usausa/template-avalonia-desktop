namespace Template.DesktopApp;

using BunnyTail.ServiceRegistration;

using Microsoft.Extensions.DependencyInjection;

using Template.DesktopApp.Views;

public static partial class Components
{
    // Navigation

    [ViewSource]
    public static partial IEnumerable<KeyValuePair<ViewId, Type>> ViewSource();

    // Services

    private const string ModuleNamespace = "Template.DesktopApp.Views";

    [ServiceRegistration(Lifetime.Transient, "View$", Namespace = ModuleNamespace)]
    [ServiceRegistration(Lifetime.Transient, "ViewModel$", Namespace = ModuleNamespace)]
    public static partial IServiceCollection AddViews(this IServiceCollection services);
}
