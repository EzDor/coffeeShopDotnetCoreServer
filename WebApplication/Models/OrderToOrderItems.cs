using System;
using System.Collections.Generic;

namespace WebApplication.Models
{
    public partial class OrderToOrderItems
    {
        public int OrderId { get; set; }
        public int OrderItemsId { get; set; }

        public Orders Order { get; set; }
        public OrderItems OrderItems { get; set; }
    }
}
