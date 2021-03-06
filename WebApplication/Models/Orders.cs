﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WebApplication.Models.Statuses;

namespace WebApplication.Models
{
    public partial class Orders
    {
        public Orders()
        {
            OrderToOrderItems = new HashSet<OrderToOrderItems>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime? CreationTime { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public OrderStatus OrderStatus { get; set; }

        public DateTime? UpdateTime { get; set; }

        public UserToOrders UserToOrders { get; set; }
        public ICollection<OrderToOrderItems> OrderToOrderItems { get; set; }
    }
}