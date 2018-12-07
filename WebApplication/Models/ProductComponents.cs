using System;
using System.Collections.Generic;

namespace WebApplication.Models
{
    public partial class ProductComponents
    {
        public int ProductId { get; set; }
        public int ProductComponentsId { get; set; }

        public Products Product { get; set; }
        public Components ProductComponentsNavigation { get; set; }
    }
}
