using System.Collections.Generic;
using WebApplication.Controllers.Forms.Cart;
using WebApplication.Models;

namespace WebApplication.Services.Interfaces
{
    public interface IOrderItemService
    {
        OrderItems CreateOrderItem(OrderForm orderForm);
        void UpdateOrderItem(int orderItemId, OrderForm orderForm, List<OrderItems> orderItems);
        void DeleteOrderItem(int orderItemId, List<OrderItems> orderItems);
        void Checkout(List<OrderItems> orderItems);
    }
}