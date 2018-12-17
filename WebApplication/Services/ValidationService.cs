using System;
using System.Linq;
using System.Text.RegularExpressions;
using WebApplication.Controllers.Forms.Cart;
using WebApplication.Controllers.Forms.Components;
using WebApplication.Controllers.Forms.Products;
using WebApplication.Controllers.Forms.Users;
using WebApplication.Services.Interfaces;

namespace WebApplication.Services
{
    public class ValidationService : IValidationService
    {
        /*********************************
        * Public Functions
        *********************************/

        public void ValidateUserForm(UserForm userForm)
        {
            if (IsUserInvalid(userForm))
            {
                throw new ApplicationException("Username and password are missing or invalid.");
            }
        }

        public void ValidateComponentForm(ComponentForm componentForm)
        {
            if (IsComponentInvalid(componentForm))
            {
                throw new ApplicationException("Some component details are missing or invalid.");
            }
        }

        public void ValidateProductForm(ProductForm productForm)
        {
            if (IsProductInvalid(productForm))
            {
                throw new ApplicationException("Some product details are missing or invalid.");
            }
        }

        public void ValidateOrderForm(OrderForm orderForm)
        {
            if (IsOrderInvalid(orderForm))
            {
                throw new ApplicationException("Some component details are missing or invalid.");
            }
        }


        public void ValidateCreditCardForm(CreditCardForm creditCardForm)
        {
            if (IsCreditCardInvalid(creditCardForm))
            {
                throw new ApplicationException("Some component details are missing or invalid.");
            }
        }


        /*********************************
        * Private Functions
        *********************************/


        private static bool IsUserInvalid(UserForm userForm)
        {
            return IsEmptyStringIncluded(userForm.username, userForm.password)
                   || IsContainsWhitespace(userForm.username, userForm.password)
                   || IsContainsNotAllowedCharacters(userForm.username);
        }

        private static bool IsComponentInvalid(ComponentForm componentForm)
        {
            return IsEmptyStringIncluded(componentForm.type, componentForm.name)
                   || IsContainsWhitespace(componentForm.type)
                   || IsContainsNotAllowedCharacters(componentForm.type)
                   || componentForm.amount < 0
                   || componentForm.price < 0;
        }

        private static bool IsProductInvalid(ProductForm productForm)
        {
            return IsEmptyStringIncluded(productForm.type, productForm.name, productForm.description)
                   || IsContainsNotAllowedCharacters(productForm.type)
                   || IsContainsWhitespace(productForm.type)
                   || productForm.price < 0;
        }


        private static bool IsOrderInvalid(OrderForm orderForm)
        {
            return IsEmptyStringIncluded(orderForm.componentsTypes.ToArray())
                   || IsEmptyStringIncluded(orderForm.componentsTypes.ToArray());
        }

        private static bool IsCreditCardInvalid(CreditCardForm creditCardForm)
        {
            return IsEmptyStringIncluded(creditCardForm.creditNumber, creditCardForm.expireDate,
                creditCardForm.cvv);
        }


        private static bool IsEmptyStringIncluded(params string[] strings)
        {
            return strings.Any(string.IsNullOrEmpty);
        }

        private static bool IsContainsWhitespace(params string[] strings)
        {
            return strings.Any(str => str.Any(char.IsWhiteSpace));
        }

        private static bool IsContainsNotAllowedCharacters(params string[] strings)
        {
            return !strings.Any(str => Regex.IsMatch(str, "\\w+"));
        }
    }
}