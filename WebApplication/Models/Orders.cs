using System;
using System.Collections.Generic;

namespace WebApplication.Models
{
    public partial class Orders
    {
        public Orders()
        {
            OrderToOrderItems = new HashSet<OrderToOrderItems>();
        }

        public int Id { get; set; }
        public DateTime? CreationTime { get; set; }
        public int? OrderStatus { get; set; }
        public DateTime? UpdateTime { get; set; }

        public UserToOrders UserToOrders { get; set; }
        public ICollection<OrderToOrderItems> OrderToOrderItems { get; set; }
    }
}
