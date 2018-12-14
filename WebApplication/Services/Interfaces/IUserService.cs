using System.Threading.Tasks;
using WebApplication.Controllers.Forms;

namespace WebApplication.Services.Interfaces
{
    public interface IUserService
    {
        LoginResponseParams Login(string username, string password);
        void CreateUser(UserForm userForm);

    }
}