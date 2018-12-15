using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Controllers.Forms.Users;
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
        private readonly IValidationService _validationService;

        public UsersController(IUserService userService, IValidationService validationService)
        {
            _userService = userService;
            _validationService = validationService;
        }


        [HttpGet]
        [Authorize(Roles = Constants.ADMIN_ROLE)]
        public ActionResult<List<Users>> GetUsers()
        {
            return Ok(_userService.GetUsers());
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult<LoginResponseParams> Login([FromBody] LoginRequestParams loginRequestParams)
        {
            return Ok(_userService.Login(loginRequestParams.username, loginRequestParams.password));
        }

        [HttpPost("signUp")]
        [AllowAnonymous]
        public ActionResult<Status> CreateUser([FromBody] UserForm userForm)
        {
            var isAdminRequest = HttpContext.User.IsInRole(Constants.ADMIN_ROLE);
            _validationService.ValidateUserForm(userForm);
            _userService.CreateUser(userForm, isAdminRequest);
            return Ok(new Status("User created successfully"));
        }

        [HttpPost("update")]
        public ActionResult<Status> UpdateUser([FromBody] UpdatedUserForm updatedUserForm)
        {
            var isAdminRequest = HttpContext.User.IsInRole(Constants.ADMIN_ROLE);
            _validationService.ValidateUserForm(updatedUserForm.updatedUserDetails);
            _userService.UpdateUser(updatedUserForm, isAdminRequest);
            return Ok(new Status("User updated successfully"));
        }

        [HttpPost("delete")]
        [Authorize(Roles = Constants.ADMIN_ROLE)]
        public ActionResult<Status> DeleteUser([FromBody] DeleteUserRequestParams deleteUserRequestParams)
        {
            _userService.DeleteUser(deleteUserRequestParams.username);
            return Ok(new Status("User deleted successfully"));
        }
    }
}