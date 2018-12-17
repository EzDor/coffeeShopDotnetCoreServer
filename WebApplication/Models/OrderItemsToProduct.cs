using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WebApplication.Models
{
    public partial class OrderItemsToProduct
    {
        [ForeignKey("ProductId")]
        public int? ProductId { get; set; }
        [ForeignKey("OrderId")]
        public int Id { get; set; }

        [JsonProperty("OrderItems")]
        public OrderItems IdNavigation { get; set; }
        [JsonProperty("product")]
        public Products Product { get; set; }
    }
}
