using System;
using System.Collections.Generic;

namespace WebApplication.Models
{
    public partial class UserToOrders
    {
        public int? UserId { get; set; }
        public int Id { get; set; }

        public Orders IdNavigation { get; set; }
        public Users User { get; set; }
    }
}
