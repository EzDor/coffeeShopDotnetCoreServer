using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public partial class OrderItems
    {
        public OrderItems()
        {
            OrderItemToComponents = new HashSet<OrderItemToComponents>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public double? Price { get; set; }

        public OrderItemsToProduct OrderItemsToProduct { get; set; }
        public OrderToOrderItems OrderToOrderItems { get; set; }
        public ICollection<OrderItemToComponents> OrderItemToComponents { get; set; }
    }
}
