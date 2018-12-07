using Microsoft.Extensions.DependencyInjection;

namespace WebApplication.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            // services.AddTransient<ITopicAreaService, TopicAreaService>();
            // Add all other repositories here.
            return services;
        }
    }
}