using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Controllers.Forms.Cart;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;
using WebApplication.Services.Interfaces;

namespace WebApplication.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IProductService _productService;
        private readonly IComponentService _componentService;

        public OrderItemService(IOrderItemRepository orderItemRepository, IProductService productService,
            IComponentService componentService)
        {
            _orderItemRepository = orderItemRepository;
            _productService = productService;
            _componentService = componentService;
        }
        /*********************************
        * Public Functions
        *********************************/

        public OrderItems CreateOrderItem(OrderForm orderForm)
        {
            var orderItem = new OrderItems();
            PrepareOrderItem(orderForm, orderItem);

            return orderItem;
        }

        public void UpdateOrderItem(int orderItemId, OrderForm orderForm, List<OrderItems> orderItems)
        {
            var orderItem = GetAndValidateOrderItem(orderItemId, orderItems);
            PrepareOrderItem(orderForm, orderItem);
        }

        public void DeleteOrderItem(int orderItemId, List<OrderItems> orderItems)
        {
            var orderItem = GetAndValidateOrderItem(orderItemId, orderItems);
            _orderItemRepository.RemoveOrderItemAndRelations(orderItem);
        }

        public void Checkout(List<OrderItems> orderItems)
        {
            foreach (var orderItem in orderItems)
            {
                var components = orderItem.OrderItemToComponents.Select(component => component.Components).ToList();
                _componentService.DecreaseAmount(components);
            }
        }

        /*********************************
        * Private Functions
        *********************************/

        private OrderItems PrepareOrderItem(OrderForm orderForm, OrderItems orderItem)
        {
            var orderItemComponents = GetComponentsByTypes(orderForm.componentsTypes);
            var orderItemProduct = GetProduct(orderForm.productType);
            var price = GetItemOrderPrice(orderItemProduct, orderItemComponents);
            ComponentsToProductValidation(orderItemProduct, orderItemComponents);
            orderItem.OrderItemToComponents = orderItemComponents;
            orderItem.OrderItemsToProduct = orderItemProduct;
            orderItem.Price = price;
            _orderItemRepository.SaveChanges();
            return orderItem;
        }

        private List<OrderItemToComponents> GetComponentsByTypes(List<string> componentsTypes)
        {
            var orderItemComponents = new List<OrderItemToComponents>();
            var components = _componentService.GetComponentsByType(componentsTypes);
            if (components.Count != componentsTypes.Count)
            {
                throw new ApplicationException(
                    "Cannot update product, some components is not exist, discarded or out of" +
                    " stock. Please make sure all the components is ready to use");
            }

            foreach (var component in components)
            {
                var orderItemComponent = new OrderItemToComponents()
                {
                    Components = component
                };

                orderItemComponents.Add(orderItemComponent);
            }

            return orderItemComponents;
        }

        private OrderItemsToProduct GetProduct(string productType)
        {
            return new OrderItemsToProduct()
            {
                Product = _productService.GetProduct(productType)
            };
        }

        private static double GetItemOrderPrice(OrderItemsToProduct product,
            IEnumerable<OrderItemToComponents> components)
        {
            var price = components.Aggregate(product.Product.Price,
                (current, component) => current + component.Components.Price);

            return price;
        }

        private static void ComponentsToProductValidation(OrderItemsToProduct product,
            IEnumerable<OrderItemToComponents> components)
        {
            var productComponents = product.Product.ProductComponents
                .Select(productComponent => productComponent.ProductComponentsNavigation).ToList();
            var orderItemComponents = components.Select(component => component.Components).ToList();
            if (!orderItemComponents.All(item => productComponents.Contains(item)))
            {
                throw new ApplicationException("Cannot update order item, some components is part of this product.");
            }
        }

        private OrderItems GetAndValidateOrderItem(int orderItemId, List<OrderItems> orderItems)
        {
            var orderItem = _orderItemRepository.Get(orderItemId);
            if (!orderItems.Contains(orderItem))
            {
                throw new ApplicationException(
                    "Cannot delete order item, this item is not part of the previous order.");
            }

            return orderItem;
        }
    }
}