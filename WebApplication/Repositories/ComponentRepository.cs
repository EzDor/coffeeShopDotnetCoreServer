using System.Collections.Generic;
using WebApplication.Models;
using WebApplication.Models.Statuses;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Repositories
{
    public class ComponentRepository : Repository<Components>, IComponentRepository
    {
        public ComponentRepository(CoffeeShopDotNetContext coffeeShopDotNetContext) : base(coffeeShopDotNetContext)
        {
        }

        public Components FindByType(string type)
        {
            return GetSingleOrDefault(component => component.Type.Equals(type.ToLower()));
        }

        public IEnumerable<Components> FindAllByTypeInAndStatus(List<string> components, ComponentStatus status)
        {
            return Find(component => components.Contains(component.Type) && component.Status == status);
        }
    }
}