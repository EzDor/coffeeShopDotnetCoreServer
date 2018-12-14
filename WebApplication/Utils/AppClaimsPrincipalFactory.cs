using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using WebApplication.Models;

namespace WebApplication.Utils
{
    public class AppClaimsPrincipalFactory : UserClaimsPrincipalFactory<Users, IdentityRole>
    {
        public AppClaimsPrincipalFactory(UserManager<Users> userManager, RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor) : base(userManager, roleManager, optionsAccessor)
        {
        }

        public override async Task<ClaimsPrincipal> CreateAsync(Users user)
        {
            var principal = await base.CreateAsync(user);


            ((ClaimsIdentity) principal.Identity).AddClaims(new[]
            {
                new Claim(ClaimTypes.GivenName, user.FirstName)
            });


            ((ClaimsIdentity) principal.Identity).AddClaims(new[]
            {
                new Claim(ClaimTypes.Surname, user.LastName),
            });


            return principal;
        }
    }
}