using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Repositories
{
    public class ComponentRepository : Repository<Components>, IComponentRepository
    {
        public ComponentRepository(CoffeeShopDotNetContext coffeeShopDotNetContext) : base(coffeeShopDotNetContext)
        {
        }
    }
}