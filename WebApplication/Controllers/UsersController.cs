using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Controllers.Forms;
using WebApplication.Models;
using WebApplication.Services.Interfaces;
using WebApplication.Utils;

namespace WebApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult<LoginResponseParams> Login([FromBody] LoginRequestParams loginRequestParams)
        {
            return Ok(_userService.Login(loginRequestParams.username, loginRequestParams.password));
        }

        [HttpGet]
        public ActionResult<List<Users>> GetUsers()
        {
            return Ok();
        }


        [HttpPost("signUp")]
        [AllowAnonymous]
        public ActionResult<Status> CreateUser([FromBody] UserForm userForm)
        {
            _userService.CreateUser(userForm);
            return  Ok(new Status("User created successfully"));
        }
    }
}