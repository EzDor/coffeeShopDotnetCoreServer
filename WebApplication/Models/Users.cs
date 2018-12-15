using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WebApplication.Models.Statuses;

namespace WebApplication.Models
{
    public partial class Users
    {
        public Users()
        {
            UserToOrders = new HashSet<UserToOrders>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string FirstName { get; set; }
        [JsonProperty("admin")] public bool? IsAdmin { get; set; }
        public string LastName { get; set; }
        [JsonIgnore] public string Password { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public UserStatus? Status { get; set; }

        public string Username { get; set; }
        [JsonIgnore] public ICollection<UserToOrders> UserToOrders { get; set; }
    }
}