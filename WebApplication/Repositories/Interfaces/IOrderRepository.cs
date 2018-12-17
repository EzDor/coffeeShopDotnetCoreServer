using System.Collections.Generic;
using WebApplication.Models;
using WebApplication.Models.Statuses;

namespace WebApplication.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Orders>
    {

        Orders FindByUserAndOrderStatus(Users user, OrderStatus orderStatus);
        IEnumerable<Orders> FindAllByUserAndOrderStatus(Users user, OrderStatus orderStatus);
        void RemoveOrderToOrderItemRelation(OrderToOrderItems orderToOrderItems);

    }
}