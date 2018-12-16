using System;
using System.Collections.Generic;
using WebApplication.Controllers.Forms.Products;
using WebApplication.Models;

namespace WebApplication.Services.Interfaces
{
    public interface IProductService
    {
        List<Products> GetActiveProducts();
        List<Products> GetProducts();
        void CreateProduct(ProductForm productForm);
        Products GetProduct(string productType);
        void UpdateProduct(UpdatedProductForm updatedProductForm);
        void DeleteProduct(string type);
        
    }
}