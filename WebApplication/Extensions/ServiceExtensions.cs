using Microsoft.Extensions.DependencyInjection;
using WebApplication.Services;
using WebApplication.Services.Interfaces;

namespace WebApplication.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            // services.AddTransient<ITopicAreaService, TopicAreaService>();
            // Add all other services here.
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}