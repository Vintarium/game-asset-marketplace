using AssetMarketplace.Domain.Entities;
using AssetMarketplace.Domain.Interfaces;
using AssetMarketplace.Infrastructure.Repositories;
using AssetMarketplace.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AssetMarketplace.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IRepository<User>, BaseRepository<User>>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        return services;
    }
}