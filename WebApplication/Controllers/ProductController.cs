using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Controllers.Forms.Products;
using WebApplication.Models;
using WebApplication.Services.Interfaces;
using WebApplication.Utils;

namespace WebApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IValidationService _validationService;

        public ProductController(IProductService productService, IValidationService validationService)
        {
            _productService = productService;
            _validationService = validationService;
        }
        
        [HttpGet]
        [Authorize(Roles = Constants.ADMIN_ROLE)]
        public ActionResult<List<Products>> GetProducts()
        {
            return Ok(_productService.GetProducts());
        }
        
        [HttpGet("active")]
        public ActionResult<List<Products>> GetActiveProducts()
        {
            return Ok(_productService.GetActiveProducts());
        }
        
        [HttpPost("create")]
        [Authorize(Roles = Constants.ADMIN_ROLE)]
        public ActionResult<Status> CreateProduct([FromBody] ProductForm productForm)
        {
            _validationService.ValidateProductForm(productForm);
            _productService.CreateProduct(productForm);
            return Ok(new Status("Product is created successfully"));
        }
        
        [HttpPost("update")]
        [Authorize(Roles = Constants.ADMIN_ROLE)]
        public ActionResult<Status> UpdateComponent([FromBody] UpdatedProductForm updatedProductForm)
        {
            _validationService.ValidateProductForm(updatedProductForm.updatedProductDetails);
            _productService.UpdateProduct(updatedProductForm);
            return Ok(new Status("Product is updated successfully"));
        }
        
        [HttpPost("delete")]
        [Authorize(Roles = Constants.ADMIN_ROLE)]
        public ActionResult<Status> DeleteProduct([FromBody] DeleteProductRequestForm deleteProductRequestForm)
        {
            _productService.DeleteProduct(deleteProductRequestForm.type);
            return Ok(new Status("Product is deleted successfully"));
        }
    }
}