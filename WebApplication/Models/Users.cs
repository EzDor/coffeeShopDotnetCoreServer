using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int UserId { get; set; }
        
        public DateTime? CreationTime { get; set; }
        public string FirstName { get; set; }
        public bool? IsAdmin { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public UserStatus? Status { get; set; }
        public string Username { get; set; }

        public ICollection<UserToOrders> UserToOrders { get; set; }
    }
}
