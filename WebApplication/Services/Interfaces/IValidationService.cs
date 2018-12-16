using WebApplication.Controllers.Forms.Components;
using WebApplication.Controllers.Forms.Products;
using WebApplication.Controllers.Forms.Users;

namespace WebApplication.Services.Interfaces
{
    public interface IValidationService
    {
        void ValidateUserForm(UserForm userForm);
        void ValidateComponentForm(ComponentForm componentForm);
        void ValidateProductForm(ProductForm productForm);
    }
}