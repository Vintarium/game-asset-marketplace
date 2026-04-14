using AssetMarketplace.API.Filters;
using AssetMarketplace.Application.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace AssetMarketplace.API.Extensions;

public static class PresentationServiceExtensions
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<ValidateModelFilter>();
        });

        services.AddFluentValidationAutoValidation()
            .AddValidatorsFromAssemblyContaining<CreateUserDtoValidator>();

        return services;
    }
}
