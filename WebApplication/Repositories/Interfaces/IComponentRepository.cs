using System.Collections.Generic;
using WebApplication.Models;
using WebApplication.Models.Statuses;

namespace WebApplication.Repositories.Interfaces
{
    public interface IComponentRepository : IRepository<Components>
    {
        Components FindByType(string type);
        IEnumerable<Components> FindAllByTypeInAndStatus(List<string> components, ComponentStatus status);
    }
}