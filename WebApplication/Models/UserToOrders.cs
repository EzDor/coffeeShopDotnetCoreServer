using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WebApplication.Models
{
    public partial class UserToOrders
    {
        [ForeignKey("UserId")] public int? UserId { get; set; }
        [ForeignKey("OrderId")] public int Id { get; set; }

        [JsonIgnore] public Orders IdNavigation { get; set; }
        [JsonProperty("user")] public Users User { get; set; }
    }
}