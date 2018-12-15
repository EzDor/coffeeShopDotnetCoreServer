using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApplication.Controllers.Forms;
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

        public LoginResponseParams Login(string username, string password)
        {
            var user = _userRepository.FindByUsernameAndStatus(username.ToLower(), UserStatus.ACTIVE);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

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

        public void CreateUser(UserForm userForm)
        {
            var user = _userRepository.FindByUsernameAndStatus(userForm.username.ToLower(), UserStatus.ACTIVE);
            if (user != null)
            {
                throw new ApplicationException("User is already exist");
            }

            user = new Users
            {
                Username = userForm.username,
                FirstName = userForm.firstName,
                LastName = userForm.lastName,
                Status = UserStatus.ACTIVE,
                Password = PasswordEncoder.Encode(userForm.password),
                IsAdmin = false
            };

            _userRepository.Add(user);
            _userRepository.SaveChanges();
        }


        private object GenerateJwtToken(Users user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("roles", GetUserRole(user)),
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
    }
}