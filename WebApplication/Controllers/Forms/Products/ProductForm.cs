using System.Collections.Generic;
using WebApplication.Models.Statuses;

namespace WebApplication.Controllers.Forms.Products
{
    public class ProductForm
    {
        public string name;
        public string type;
        public string description;
        public double price;
        public ProductStatus status;
        public List<string> componentsTypes;
        public string image;
    }
}