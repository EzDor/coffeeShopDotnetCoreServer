using System.Collections.Generic;
using WebApplication.Models;
using WebApplication.Models.Statuses;

namespace WebApplication.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<Products>
    {

        IEnumerable<Products> GetAllProducts();
        IEnumerable<Products> GetAllProductsByStatus(ProductStatus status);
        Products FindByType(string type);
        void RemoveProductComponent(Products products);

    }
}