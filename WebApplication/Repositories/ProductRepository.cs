using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;
using WebApplication.Models.Statuses;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Repositories
{
    public class ProductRepository : Repository<Products>, IProductRepository
    {
        public ProductRepository(CoffeeShopDotNetContext coffeeShopDotNetContext) : base(coffeeShopDotNetContext)
        {
        }

        public IEnumerable<Products> GetAllProducts()
        {
            return _coffeeShopDotNetContext.Products
                .Include(p => p.ProductComponents)
                .ThenInclude(pc => pc.ProductComponentsNavigation)
                .OrderBy(pr => pr.Id)
                .ToList();
        }

        public IEnumerable<Products> GetAllProductsByStatus(ProductStatus status)
        {
            return _coffeeShopDotNetContext.Products
                .Where(prod => prod.Status == status)
                .Include(p => p.ProductComponents)
                .ThenInclude(pc => pc.ProductComponentsNavigation)
                .OrderBy(pr => pr.Id)
                .ToList();
        }

        public Products FindByType(string type)
        {
            return _coffeeShopDotNetContext.Products
                .Include(p => p.ProductComponents)
                .ThenInclude(pc => pc.ProductComponentsNavigation)
                .SingleOrDefault(product => product.Type.Equals(type.ToLower()));
        }

        public void RemoveProductComponent(Products product)
        {
            _coffeeShopDotNetContext.ProductComponents.RemoveRange(product.ProductComponents);
        }
    }
}