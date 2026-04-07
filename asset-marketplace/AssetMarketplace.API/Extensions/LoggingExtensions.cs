using Serilog;
using Serilog.Events;

namespace AssetMarketplace.API.Extensions;

public static class LoggingExtensions
{
    public static void ConfigureLogging()
    {
        Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Information()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .MinimumLevel.Override("System", LogEventLevel.Warning)
        .WriteTo.Console()
        .WriteTo.File(
            "Logs/errors-.txt",
             rollingInterval: RollingInterval.Day,
             restrictedToMinimumLevel: LogEventLevel.Error)
        .CreateLogger();
    }
}
