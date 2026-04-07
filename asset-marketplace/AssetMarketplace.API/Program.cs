using AssetMarketplace.API.Extensions;
using AssetMarketplace.API.Middleware;
using AssetMarketplace.Application.Extensions;
using AssetMarketplace.Infrastructure.Extensions;
using Serilog;

LoggingExtensions.ConfigureLogging();

try
{
    Log.Information("Application is starting...");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    builder.Services.AddInfrastructure(builder.Configuration);

    builder.Services.AddApplication();

    builder.Services.AddPresentation();

    builder.Services.AddSwaggerDocumentation();

    var app = builder.Build();

    app.UseMiddleware<GlobalExceptionMiddleware>();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwaggerDocumentation();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    Log.Fatal(exception, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}
