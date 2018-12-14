using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Repositories
{
    public class OrderItemRepository : Repository<OrderItems>, IOrderItemRepository
    {
        public OrderItemRepository(CoffeeShopDotNetContext coffeeShopDotNetContext) : base(coffeeShopDotNetContext)
        {
        }
    }
}