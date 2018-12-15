using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApplication.Controllers.Forms.Users;
using WebApplication.Models;
using WebApplication.Models.Statuses;
using WebApplication.Repositories.Interfaces;
using WebApplication.Services.Interfaces;
using WebApplication.Utils;

namespace WebApplication.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;

        private readonly IUserRepository _userRepository;

        public UserService(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        /*********************************
        * Public Functions
        *********************************/

        public LoginResponseParams Login(string username, string password)
        {
            var user = GetActiveUser(username);

            IsUserExist(user);

            if (!PasswordEncoder.Match(user.Password, password))
            {
                throw new ApplicationException("Username or password are incorrect");
            }

            var loginParams = new LoginResponseParams
            {
                username = username, isAdmin = user.IsAdmin, token = GenerateJwtToken(user)
            };

            return loginParams;
        }

        public void CreateUser(UserForm userForm, bool isAdminRequest)
        {
            var user = new Users();
            IsUserNameExist(userForm.username);
            PrepareUser(user, userForm, isAdminRequest, true);
            _userRepository.Add(user);
            _userRepository.SaveChanges();
        }


        public List<Users> GetUsers()
        {
            return _userRepository.GetAll().ToList();
        }

        public void UpdateUser(UpdatedUserForm updatedUserForm, bool isAdminRequest)
        {
            var user = GetUserToUpdate(updatedUserForm.usernameToUpdate, isAdminRequest);
            IsUserExist(user);
            if (!isAdminRequest && !PasswordEncoder.Match(user.Password, updatedUserForm.password))
            {
                throw new ApplicationException("Cannot update user, username or password are incorrect");
            }

            PrepareUser(user, updatedUserForm.updatedUserDetails, isAdminRequest, !isAdminRequest);
            _userRepository.SaveChanges();
        }

        public void DeleteUser(string username)
        {
            var user = GetUser(username);
            IsUserExist(user);
            user.Status = UserStatus.DISCARDED;
            _userRepository.SaveChanges();
        }

        public Users GetActiveUser(string username)
        {
            return _userRepository.FindByUsernameAndStatus(username.ToLower(), UserStatus.ACTIVE);
        }


        /*********************************
        * Private Functions
        *********************************/

        private object GenerateJwtToken(Users user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, GetUserRole(user)),
                new Claim(Constants.role_key, GetUserRole(user)),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string GetUserRole(Users user)
        {
            string role;
            if (user.IsAdmin != null && user.IsAdmin.Value)
            {
                role = Constants.ADMIN_ROLE;
            }
            else
            {
                role = Constants.USER_ROLE;
            }

            return role;
        }

        private static void IsUserExist(Users user)
        {
            if (user == null)
            {
                throw new ApplicationException("User is not exist");
            }
        }

        private void IsUserNameExist(string username)
        {
            if (GetActiveUser(username) != null)
            {
                throw new ApplicationException("User " + username + " is already exist");
            }
        }

        private Users GetUser(string username)
        {
            return _userRepository.FindByUsername(username.ToLower());
        }


        private Users GetUserToUpdate(string username, bool isAdminRequest)
        {
            return isAdminRequest ? GetUser(username) : GetActiveUser(username);
        }

        private Users PrepareUser(Users user, UserForm userForm, bool isAdminRequest, bool updatePassword)
        {
            user.Username = userForm.username;
            user.FirstName = userForm.firstName;
            user.LastName = userForm.lastName;
            user.Status = userForm.status;
            user.IsAdmin = userForm.admin && isAdminRequest;

            if (updatePassword)
            {
                user.Password = PasswordEncoder.Encode(userForm.password);
            }

            return user;
        }
    }
}