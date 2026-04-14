using AssetMarketplace.Application.Interfaces;
using AssetMarketplace.Application.Mappings;
using AssetMarketplace.Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AssetMarketplace.Application.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddScoped<IUserService, UserService>()
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
            .AddAutoMapper(cfg =>
            {
                cfg.AddProfile<UserMappingProfile>();
            });

        return services;
    }
}
