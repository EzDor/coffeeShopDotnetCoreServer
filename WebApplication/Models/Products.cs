using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WebApplication.Models.Statuses;

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
        public string Type { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ProductStatus Status { get; set; }

        [JsonIgnore] public ICollection<OrderItemsToProduct> OrderItemsToProduct { get; set; }

        public ICollection<ProductComponents> ProductComponents { get; set; }
    }
}