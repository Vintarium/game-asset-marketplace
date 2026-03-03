using AssetMarketplace.Domain.Entities;
using AssetMarketplace.Domain.Interfaces;
using AssetMarketplace.Infrastructure.Repositories;
using AssetMarketplace.Infrastructure.Security;
using AssetMarketplace.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AssetMarketplace.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<DatabaseOptions>()
            .Bind(configuration.GetSection(DatabaseOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddDbContext<ApplicationDbContext>((serviceProvider, dbContextOptions) =>
        {
            var databaseSettings = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;

            dbContextOptions.UseNpgsql(databaseSettings.DefaultConnection);
        });

        services.AddScoped<IRepository<User>, BaseRepository<User>>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        return services;
    }
}
