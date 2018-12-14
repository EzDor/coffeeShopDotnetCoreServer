using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication.Repositories;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Extensions
{
    public static class RepositoryExtensions
    {
        
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IComponentRepository, ComponentRepository>();
       
            return services;
        }
    }
}