using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Controllers.Forms.Cart;
using WebApplication.Models;
using WebApplication.Services.Interfaces;
using WebApplication.Utils;

namespace WebApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly IValidationService _validationService;
        private readonly IOrderService _orderService;

        public CartController(IValidationService validationService, IOrderService orderService)
        {
            _validationService = validationService;
            _orderService = orderService;
        }

        [HttpGet]
        public ActionResult<Orders> GetActiveOrder()
        {
            var username = GetUsername();
            return Ok(_orderService.GetCartOrder(username));
        }

        [HttpGet("archive")]
        public ActionResult<List<Orders>> GetArchiveOrders()
        {
            var username = GetUsername();
            return Ok(_orderService.GetUserArchiveOrders(username));
        }


        [HttpPost("add")]
        public ActionResult<Status> AddItemToOrder([FromBody] OrderForm orderForm)
        {
            var username = GetUsername();
            _validationService.ValidateOrderForm(orderForm);
            _orderService.AddItemToOrder(orderForm, username);
            return Ok(new Status("Item is added to cart successfully"));
        }

        [HttpPost("update")]
        public ActionResult<Status> UpdateOrderItem(UpdateOrderForm updateOrderForm)
        {
            var username = GetUsername();
            _validationService.ValidateOrderForm(updateOrderForm.orderDetails);
            _orderService.UpdateOrderItem(updateOrderForm, username);
            return Ok(new Status("Item is updated to cart successfully"));
        }

        [HttpPost("delete")]
        public ActionResult<Status> DeleteOrderItem([FromBody] DeleteOrderItemRequestForm deleteOrderItemRequestForm)
        {
            var username = GetUsername();
            _orderService.DeleteOrderItem(deleteOrderItemRequestForm.id, username);
            return Ok(new Status("Item is deleted to cart successfully"));
        }
        
        [HttpPost("checkout")]
        public ActionResult<Status> CheckOut([FromBody] CreditCardForm creditCardForm)
        {
            var username = GetUsername();
            _validationService.ValidateCreditCardForm(creditCardForm);
            _orderService.Checkout(username);
            return Ok(new Status("Item is deleted to cart successfully"));
        }


        private string GetUsername()
        {
            return HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sub).Value;
        }
    }
}