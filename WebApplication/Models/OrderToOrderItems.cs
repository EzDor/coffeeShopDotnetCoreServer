using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WebApplication.Models
{
    public partial class OrderToOrderItems
    {
        [ForeignKey("OrderId")] public int OrderId { get; set; }
        [ForeignKey("OrderItemId")] public int OrderItemsId { get; set; }

        [JsonIgnore] public Orders Order { get; set; }
        [JsonProperty("OrderItems")] public OrderItems OrderItems { get; set; }
    }
}