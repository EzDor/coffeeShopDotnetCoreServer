using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly CoffeeShopDotNetContext _coffeeShopDotNetContext;

        public UserController(CoffeeShopDotNetContext coffeeShopDotNetContext)
        {
            this._coffeeShopDotNetContext = coffeeShopDotNetContext;
        }

        [HttpGet]
        public ActionResult<List<Users>> GetUsers()
        {
            return _coffeeShopDotNetContext.Users.ToList();
        }
    }
}