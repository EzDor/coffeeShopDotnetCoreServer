using Microsoft.Extensions.DependencyInjection;
using WebApplication.Services;
using WebApplication.Services.Interfaces;

namespace WebApplication.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            // Add all other services here.
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IComponentService, ComponentService>();
            services.AddScoped<IValidationService, ValidationService>();
            return services;
        }
    }
}