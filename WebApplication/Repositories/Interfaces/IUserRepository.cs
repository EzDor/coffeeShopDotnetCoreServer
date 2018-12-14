using WebApplication.Models;
using WebApplication.Models.Statuses;

namespace WebApplication.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<Users>
    {

        Users FindByUsernameAndStatus(string username, UserStatus status);

    }
}