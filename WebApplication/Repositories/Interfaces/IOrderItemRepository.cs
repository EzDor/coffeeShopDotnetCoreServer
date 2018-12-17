using WebApplication.Models;

namespace WebApplication.Repositories.Interfaces
{
    public interface IOrderItemRepository : IRepository<OrderItems>
    {
        void RemoveOrderItemAndRelations(OrderItems orderItem);
    }
}