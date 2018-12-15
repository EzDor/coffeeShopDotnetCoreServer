using WebApplication.Controllers.Forms;

namespace WebApplication.Services.Interfaces
{
    public interface IValidationService
    {
        void ValidateUserForm(UserForm userForm);
    }
}