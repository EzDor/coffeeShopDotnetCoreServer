using System;
using System.Collections.Generic;

namespace WebApplication.Models
{
    public partial class OrderItemToComponents
    {
        public int OrderItemId { get; set; }
        public int ComponentsId { get; set; }

        public Components Components { get; set; }
        public OrderItems OrderItem { get; set; }
    }
}
