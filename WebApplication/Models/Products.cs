using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public partial class Products
    {
        public Products()
        {
            OrderItemsToProduct = new HashSet<OrderItemsToProduct>();
            ProductComponents = new HashSet<ProductComponents>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Description { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public int? Status { get; set; }
        public string Type { get; set; }

        public ICollection<OrderItemsToProduct> OrderItemsToProduct { get; set; }
        public ICollection<ProductComponents> ProductComponents { get; set; }
    }
}