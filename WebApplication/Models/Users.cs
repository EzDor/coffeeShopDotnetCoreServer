using System;
using System.Collections.Generic;

namespace WebApplication.Models
{
    public partial class Users
    {
        public Users()
        {
            UserToOrders = new HashSet<UserToOrders>();
        }

        public int Id { get; set; }
        public DateTime? CreationTime { get; set; }
        public string FirstName { get; set; }
        public bool? IsAdmin { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public int? Status { get; set; }
        public string Username { get; set; }

        public ICollection<UserToOrders> UserToOrders { get; set; }
    }
}
