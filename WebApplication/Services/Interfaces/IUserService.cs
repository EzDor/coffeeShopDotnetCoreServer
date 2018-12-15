using System.Collections.Generic;
using WebApplication.Controllers.Forms;
using WebApplication.Controllers.Forms.Users;
using WebApplication.Models;

namespace WebApplication.Services.Interfaces
{
    public interface IUserService
    {
        LoginResponseParams Login(string username, string password);
        void CreateUser(UserForm userForm, bool isAdminRequest);
        List<Users> GetUsers();
        void UpdateUser(UpdatedUserForm updatedUserForm, bool isAdminRequest);
        Users GetActiveUser(string username);
        void DeleteUser(string username);

    }
}