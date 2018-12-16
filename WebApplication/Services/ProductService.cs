using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Controllers.Forms.Products;
using WebApplication.Models;
using WebApplication.Models.Statuses;
using WebApplication.Repositories.Interfaces;
using WebApplication.Services.Interfaces;

namespace WebApplication.Services
{
    public class ProductService : IProductService
    {
        private readonly IComponentService _componentService;
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository, IComponentService componentService)
        {
            _productRepository = productRepository;
            _componentService = componentService;
        }

        /*********************************
        * Public Functions
        *********************************/

        public List<Products> GetActiveProducts()
        {
            return _productRepository.GetAllProductsByStatus(ProductStatus.ACTIVE).ToList();
        }

        public List<Products> GetProducts()
        {
            return _productRepository.GetAllProducts().ToList();
        }

        public void CreateProduct(ProductForm productForm)
        {
            var product = GetProduct(productForm.type);
            if (product != null)
            {
                throw new ApplicationException("Cannot create product, product with type " + productForm.type +
                                               " is already exist");
            }

            product = new Products();
            product = PrepareProduct(product, productForm);

            _productRepository.Add(product);
            _productRepository.SaveChanges();
        }

        public Products GetProduct(string productType)
        {
            return _productRepository.FindByType(productType);
        }

        public void UpdateProduct(UpdatedProductForm updatedProductForm)
        {
            var product = GetProduct(updatedProductForm.productTypeToUpdate);
            if (product == null)
            {
                throw new ApplicationException("Cannot update product, product with type " +
                                               updatedProductForm.productTypeToUpdate + " is not found");
            }

            _productRepository.RemoveProductComponent(product);
            product = PrepareProduct(product, updatedProductForm.updatedProductDetails);
            _productRepository.SaveChanges();
        }

        public void DeleteProduct(string type)
        {
            var product = GetProduct(type);
            if (product == null)
            {
                throw new ApplicationException("Cannot delete product, product with type " +
                                               type + " is not found");
            }

            product.Status = ProductStatus.DISCARDED;
            _productRepository.SaveChanges();
        }

        /*********************************
        * Private Functions
        *********************************/

        private Products PrepareProduct(Products product, ProductForm productForm)
        {
            product.Type = productForm.type;
            product.Description = productForm.description;
            product.Name = productForm.name;
            product.Price = productForm.price;
            product.Status = productForm.status;
            product.Image = productForm.image;
            product.ProductComponents = GetComponentsByTypes(productForm.componentsTypes);

            return product;
        }

        private List<ProductComponents> GetComponentsByTypes(List<string> componentsTypes)
        {
            var productComponents = new List<ProductComponents>();
            var components = _componentService.GetComponentsByType(componentsTypes);
            if (components.Count != componentsTypes.Count)
            {
                throw new ApplicationException(
                    "Cannot update product, some components is not exist, discarded or out of" +
                    " stock. Please make sure all the components is ready to use");
            }

            foreach (var component in components)
            {
                var productComponent = new ProductComponents()
                {
                    ProductComponentsNavigation = component
                };

                productComponents.Add(productComponent);
            }

            return productComponents;
        }
    }
}