using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Controllers.Forms.Cart;
using WebApplication.Models;
using WebApplication.Models.Statuses;
using WebApplication.Repositories.Interfaces;
using WebApplication.Services.Interfaces;

namespace WebApplication.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserService _userService;
        private readonly IOrderItemService _orderItemService;

        public OrderService(IOrderRepository orderRepository, IUserService userService,
            IOrderItemService orderItemService)
        {
            _orderRepository = orderRepository;
            _userService = userService;
            _orderItemService = orderItemService;
        }

        /*********************************
        * Public Functions
        *********************************/

        public Orders GetCartOrder(string username)
        {
            var user = GetUser(username);
            var order = GetActiveOrder(user) ?? new Orders();

            return order;
        }

        public List<Orders> GetUserArchiveOrders(string username)
        {
            var user = GetUser(username);
            return _orderRepository.FindAllByUserAndOrderStatus(user, OrderStatus.DONE).ToList();
        }

        public void AddItemToOrder(OrderForm orderForm, string username)
        {
            var newOrderItem = _orderItemService.CreateOrderItem(orderForm);
            var user = GetUser(username);
            var order = GetOrCreate(user);
            var orderRelation = new OrderToOrderItems()
            {
                Order = order,
                OrderItems = newOrderItem
            };
            order.OrderToOrderItems.Add(orderRelation);
            _orderRepository.SaveChanges();
        }

        public void UpdateOrderItem(UpdateOrderForm updateOrderForm, string username)
        {
            var order = GetAndValidateActiveOrder(username);
            var orderItems = GetOrderItems(order);
            _orderItemService.UpdateOrderItem(updateOrderForm.orderItemId, updateOrderForm.orderDetails, orderItems);
            order.UpdateTime = DateTime.Now;
            _orderRepository.SaveChanges();
        }

        public void DeleteOrderItem(int orderItemId, string username)
        {
            var order = GetAndValidateActiveOrder(username);
            var orderItems = GetOrderItems(order);
            var orderRelationToDelete =
                order.OrderToOrderItems.Single(orderRelation => orderRelation.OrderItemsId == orderItemId);
            _orderRepository.RemoveOrderToOrderItemRelation(orderRelationToDelete);
            _orderItemService.DeleteOrderItem(orderItemId, orderItems);
            order.UpdateTime = DateTime.Now;
            if (!orderItems.Any())
            {
                order.OrderStatus = OrderStatus.CANCELED;
            }

            _orderRepository.SaveChanges();
        }

        public void Checkout(string username)
        {
            var order = GetAndValidateActiveOrder(username);
            var orderItems = GetOrderItems(order);
            _orderItemService.Checkout(orderItems);
            order.OrderStatus = OrderStatus.DONE;
            order.UpdateTime = DateTime.Now;
            _orderRepository.SaveChanges();
        }

        /*********************************
        * Private Functions
        *********************************/

        private Users GetUser(string username)
        {
            var user = _userService.GetActiveUser(username);
            if (user == null)
            {
                throw new ApplicationException("User " + username + " not found");
            }

            return user;
        }

        private Orders GetActiveOrder(Users user)
        {
            return _orderRepository.FindByUserAndOrderStatus(user, OrderStatus.IN_PROGRESS);
        }

        private Orders GetOrCreate(Users user)
        {
            var order = GetActiveOrder(user) ?? CreateOrder(user);

            return order;
        }

        private static Orders CreateOrder(Users user)
        {
            var timestamp = DateTime.Now;
            var order = new Orders
            {
                OrderStatus = OrderStatus.IN_PROGRESS,
                UserToOrders = new UserToOrders {User = user},
                CreationTime = timestamp,
                UpdateTime = timestamp
            };
            return order;
        }

        private Orders GetAndValidateActiveOrder(string username)
        {
            var user = GetUser(username);
            var order = GetActiveOrder(user);
            if (order == null)
            {
                throw new ApplicationException("Cannot update order item, order is no found.");
            }

            return order;
        }

        private static List<OrderItems> GetOrderItems(Orders order)
        {
            return order.OrderToOrderItems.Select(orderRelation => orderRelation.OrderItems).ToList();
        }
    }
}