using Microsoft.Extensions.DependencyInjection;

namespace WebApplication.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            // services.AddTransient<ITopicAreaService, TopicAreaService>();
            // Add all other services here.
            return services;
        }
    }
}