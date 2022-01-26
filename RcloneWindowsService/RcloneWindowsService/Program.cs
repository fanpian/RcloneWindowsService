using RcloneWindowsService;
using System.Runtime.InteropServices;
using NLog.Web;
using NLog;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Info("³ÌÐò³õÊ¼»¯");

try
{
    RcloneWindowsService.Host.Init();
    IHostBuilder hostBuilder = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args);
    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    {
        hostBuilder.UseWindowsService(option =>
        {
            option.ServiceName = "Rclone Windows Service";
        });
    }
    else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
    {
        hostBuilder.UseSystemd();
    }

    IHost host = hostBuilder
        .ConfigureServices(services =>
        {
            services.AddHostedService<Worker>();
        })
        .ConfigureLogging(log=> {
            log.ClearProviders();
            log.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
        })
        .UseNLog()
        .Build();


    await host.RunAsync();
}
catch (Exception ex)
{
    logger.Error(ex);
}
finally
{
    NLog.LogManager.Shutdown();
}
