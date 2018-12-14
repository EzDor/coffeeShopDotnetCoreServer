using System;
using WebApplication.Models;
using WebApplication.Models.Statuses;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Repositories
{
    public class UserRepository : Repository<Users>, IUserRepository
    {
        public UserRepository(CoffeeShopDotNetContext coffeeShopDotNetContext) : base(coffeeShopDotNetContext)
        {
        }

        public Users FindByUsernameAndStatus(string username, UserStatus status)
        {
            return GetSingleOrDefault(user => user.Username.Equals(username.ToLower()));
        }
    }
}