namespace Template.DesktopApp;

using System.Runtime.InteropServices;

using BunnyTail.ServiceRegistration;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

using Serilog;

using Smart.Avalonia;
using Smart.Resolver;

using Template.DesktopApp.Settings;
using Template.DesktopApp.Views;

public static partial class ApplicationExtensions
{
    //--------------------------------------------------------------------------------
    // Logging
    //--------------------------------------------------------------------------------

    public static HostApplicationBuilder ConfigureLogging(this HostApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Services.AddSerilog(options =>
        {
            options.ReadFrom.Configuration(builder.Configuration);
        });

        return builder;
    }

    //--------------------------------------------------------------------------------
    // Components
    //--------------------------------------------------------------------------------

    public static HostApplicationBuilder ConfigureComponents(this HostApplicationBuilder builder)
    {
        builder.Services.AddAvaloniaServices();

        // Setting
        builder.Services.AddOptions<Setting>().BindConfiguration("Setting").ValidateDataAnnotations().ValidateOnStart();
        builder.Services.AddSingleton(static p => p.GetRequiredService<IOptions<Setting>>().Value);

        // Service
        builder.Services.AddServices();

        builder.ConfigureContainer(new SmartServiceProviderFactory(), ConfigureContainer);

        return builder;
    }

    private static void ConfigureContainer(ResolverConfig config)
    {
        config
            .UseAutoBinding()
            .UseArrayBinding()
            .UseAssignableBinding();

        // Messenger
        config.BindSingleton<IReactiveMessenger>(ReactiveMessenger.Default);

        // Store
        config.BindSingleton<UserSettingStore>();

        // Navigation
        config.BindSingleton<Navigator>(resolver =>
        {
            var navigator = new NavigatorConfig()
                .UseAvaloniaNavigationProvider()
                .UseServiceProvider(resolver)
                .UseIdViewMapper(static m => m.AutoRegister(ViewSource()))
                .ToNavigator();
#if DEBUG
            navigator.Navigated += (_, args) =>
            {
                // for debug
                System.Diagnostics.Debug.WriteLine($"Navigated: [{args.Context.FromId}]->[{args.Context.ToId}] : stacked=[{navigator.StackedCount}]");
            };
#endif

            return navigator;
        });

        // Window
        config.BindSingleton<MainWindow>();
    }

    //--------------------------------------------------------------------------------
    // Navigation
    //--------------------------------------------------------------------------------

    [ViewSource]
    public static partial IEnumerable<KeyValuePair<ViewId, Type>> ViewSource();

    //--------------------------------------------------------------------------------
    // Service
    //--------------------------------------------------------------------------------

    [ServiceRegistration(Lifetime.Singleton, "Service$")]
    public static partial IServiceCollection AddServices(this IServiceCollection services);

    //--------------------------------------------------------------------------------
    // Startup
    //--------------------------------------------------------------------------------

    public static async ValueTask StartApplicationAsync(this IHost host)
    {
        // Start host
        await host.StartAsync().ConfigureAwait(false);

        // Startup log
        var log = host.Services.GetRequiredService<ILogger<App>>();
        var environment = host.Services.GetRequiredService<IHostEnvironment>();
        ThreadPool.GetMinThreads(out var workerThreads, out var completionPortThreads);

        log.InfoStartup();
        log.InfoStartupSettingsRuntime(RuntimeInformation.OSDescription, RuntimeInformation.FrameworkDescription, RuntimeInformation.RuntimeIdentifier);
        log.InfoStartupSettingsGC(GCSettings.IsServerGC, GCSettings.LatencyMode, GCSettings.LargeObjectHeapCompactionMode);
        log.InfoStartupSettingsThreadPool(workerThreads, completionPortThreads);
        log.InfoStartupApplication(environment.ApplicationName, typeof(App).Assembly.GetName().Version);
        log.InfoStartupEnvironment(environment.EnvironmentName, environment.ContentRootPath);

        // Navigate to view
        var navigator = host.Services.GetRequiredService<Navigator>();
        await navigator.ForwardAsync(ViewId.Menu).ConfigureAwait(false);
    }

    public static async ValueTask ExitApplicationAsync(this IHost host)
    {
        // Stop host
        await host.StopAsync(TimeSpan.FromSeconds(5)).ConfigureAwait(false);
        host.Dispose();
    }
}
