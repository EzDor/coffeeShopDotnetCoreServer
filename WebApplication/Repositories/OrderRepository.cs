using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;
using WebApplication.Models.Statuses;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Repositories
{
    public class OrderRepository : Repository<Orders>, IOrderRepository
    {
        public OrderRepository(CoffeeShopDotNetContext coffeeShopDotNetContext) : base(coffeeShopDotNetContext)
        {
        }

        public Orders FindByUserAndOrderStatus(Users user, OrderStatus orderStatus)
        {
            return _coffeeShopDotNetContext.Orders
                .Include(o => o.OrderToOrderItems)
                .ThenInclude(oi => oi.OrderItems)
                .ThenInclude(orderItem => orderItem.OrderItemsToProduct)
                .ThenInclude(oip => oip.Product)
                .ThenInclude(p => p.ProductComponents)
                .ThenInclude(pc => pc.ProductComponentsNavigation)
                
                .Include(o => o.OrderToOrderItems)
                .ThenInclude(oi => oi.OrderItems)
                .ThenInclude(orderItem => orderItem.OrderItemToComponents)
                .ThenInclude(oic => oic.Components)
                
                .Include(o => o.UserToOrders)
                .ThenInclude(uto => uto.User)
                
                .SingleOrDefault(o => o.UserToOrders.UserId == user.Id && o.OrderStatus == orderStatus);
        }

        public IEnumerable<Orders> FindAllByUserAndOrderStatus(Users user, OrderStatus orderStatus)
        {
            return _coffeeShopDotNetContext.Orders
                .Include(o => o.OrderToOrderItems)
                .ThenInclude(oi => oi.OrderItems)
                .ThenInclude(orderItem => orderItem.OrderItemsToProduct)
                .ThenInclude(oip => oip.Product)
                .ThenInclude(p => p.ProductComponents)
                .ThenInclude(pc => pc.ProductComponentsNavigation)
                
                .Include(o => o.OrderToOrderItems)
                .ThenInclude(oi => oi.OrderItems)
                .ThenInclude(orderItem => orderItem.OrderItemToComponents)
                .ThenInclude(oic => oic.Components)
                
                .Include(o => o.UserToOrders)
                .ThenInclude(uto => uto.User)
                
                .Where(o => o.UserToOrders.UserId == user.Id && o.OrderStatus == orderStatus)
                .OrderByDescending(o => o.Id)
                .ToList();
        }

        public void RemoveOrderToOrderItemRelation(OrderToOrderItems orderToOrderItems)
        {
            _coffeeShopDotNetContext.OrderToOrderItems.Remove(orderToOrderItems);
        }
    }
}