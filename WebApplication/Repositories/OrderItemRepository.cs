using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Repositories
{
    public class OrderItemRepository : Repository<OrderItems>, IOrderItemRepository
    {
        public OrderItemRepository(CoffeeShopDotNetContext coffeeShopDotNetContext) : base(coffeeShopDotNetContext)
        {
        }

        public void RemoveOrderItemAndRelations(OrderItems orderItem)
        {
            _coffeeShopDotNetContext.OrderItemsToProduct.Remove(orderItem.OrderItemsToProduct);
            _coffeeShopDotNetContext.OrderItemToComponents.RemoveRange(orderItem.OrderItemToComponents);
            _coffeeShopDotNetContext.OrderItems.Remove(orderItem);
        }
    }
}