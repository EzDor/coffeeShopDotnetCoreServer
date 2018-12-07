using System;
using System.Collections.Generic;

namespace WebApplication.Models
{
    public partial class OrderItems
    {
        public OrderItems()
        {
            OrderItemToComponents = new HashSet<OrderItemToComponents>();
        }

        public int Id { get; set; }
        public double? Price { get; set; }

        public OrderItemsToProduct OrderItemsToProduct { get; set; }
        public OrderToOrderItems OrderToOrderItems { get; set; }
        public ICollection<OrderItemToComponents> OrderItemToComponents { get; set; }
    }
}
