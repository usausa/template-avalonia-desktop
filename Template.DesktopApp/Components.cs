namespace Template.DesktopApp;

using BunnyTail.ServiceRegistration;

using Microsoft.Extensions.DependencyInjection;

using Template.DesktopApp.Modules;

public static partial class Components
{
    // Navigation

    [ViewSource]
    public static partial IEnumerable<KeyValuePair<ViewId, Type>> ViewSource();

    // Services

    // TODO

    //[ServiceRegistration(Lifetime.Transient, "View$", Namespace = "Template.DesktopApp.Modules")]
    [ServiceRegistration(Lifetime.Transient, "View$")]
    public static partial IServiceCollection AddViews(this IServiceCollection services);

    //[ServiceRegistration(Lifetime.Transient, "ViewModel$", Namespace = "Template.DesktopApp.Modules")]
    [ServiceRegistration(Lifetime.Transient, "ViewModel$")]
    public static partial IServiceCollection AddViewModels(this IServiceCollection services);
}
