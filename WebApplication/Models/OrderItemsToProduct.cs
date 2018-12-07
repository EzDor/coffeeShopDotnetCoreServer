using System;
using System.Collections.Generic;

namespace WebApplication.Models
{
    public partial class OrderItemsToProduct
    {
        public int? ProductId { get; set; }
        public int Id { get; set; }

        public OrderItems IdNavigation { get; set; }
        public Products Product { get; set; }
    }
}
