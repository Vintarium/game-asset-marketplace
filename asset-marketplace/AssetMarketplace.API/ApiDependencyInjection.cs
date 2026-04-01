using AssetMarketplace.API.Filters;
using AssetMarketplace.Application.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace AssetMarketplace.API
{
    public static class ApiDependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<ValidateModelFilter>();
                options.Filters.Add<ExceptionFilter>();
            });

            services.AddFluentValidationAutoValidation()
            .AddValidatorsFromAssemblyContaining<CreateUserDtoValidator>();

            return services;
        }
    }
}
