using System.Collections.Generic;
using WebApplication.Controllers.Forms.Cart;
using WebApplication.Models;

namespace WebApplication.Services.Interfaces
{
    public interface IOrderService
    {
        Orders GetCartOrder(string username);
        List<Orders> GetUserArchiveOrders(string username);
        void AddItemToOrder(OrderForm orderForm, string username);
        void UpdateOrderItem(UpdateOrderForm updateOrderForm, string username);
        void DeleteOrderItem(int orderItemId, string username);
        void Checkout(string username);
    }
}