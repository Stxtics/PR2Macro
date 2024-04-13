using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PR2Macro.Interfaces;
using PR2Macro.Searches.Titles;
using PR2Macro.Searches.Usernames;
using PR2Macro.Services;

namespace PR2Macro;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();

        IHost host = CreateHostBuilder().Build();
        ServiceProvider = host.Services;

        Application.Run(ServiceProvider.GetRequiredService<MainForm>());
    }

    public static IServiceProvider? ServiceProvider { get; private set; }

    private static IHostBuilder CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                _ = services.AddTransient<IHotkeyService, HotkeyService>();
                _ = services.AddTransient<IMacroService, MacroService>();
                _ = services.AddTransient<ISimmingService, SimmingService>();
                _ = services.AddTransient<IResourcesService, ResourcesService>();
                _ = services.AddTransient<MainForm>();
                _ = services.AddTransient<AddAccountForm>();
                _ = services.AddTransient<RemoveAccountForm>();
                _ = services.AddTransient<UpdateAccountForm>();
                _ = services.AddTransient<AddTitleForm>();
                _ = services.AddTransient<RemoveTitleForm>();
                _ = services.AddTransient<UpdateTitleForm>();
                _ = services.AddTransient<AddUsernameForm>();
                _ = services.AddTransient<RemoveUsernameForm>();
                _ = services.AddTransient<UpdateUsernameForm>();
            });
    }
}
