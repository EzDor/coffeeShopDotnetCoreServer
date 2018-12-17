using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WebApplication.Models
{
    public partial class OrderItemToComponents
    {
        [ForeignKey("OrderItemId")] public int OrderItemId { get; set; }
        [ForeignKey("ComponentsId")] public int ComponentsId { get; set; }
        [JsonProperty("Components")] public Components Components { get; set; }
        [JsonIgnore] public OrderItems OrderItem { get; set; }
    }
}