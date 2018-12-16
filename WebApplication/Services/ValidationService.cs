using System;
using System.Linq;
using System.Text.RegularExpressions;
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


        /*********************************
        * Private Functions
        *********************************/


        private bool IsUserInvalid(UserForm userForm)
        {
            return isEmptyStringIncluded(userForm.username, userForm.password)
                   || isContainsWhitespace(userForm.username, userForm.password)
                   || isContainsNotAllowedCharacters(userForm.username);
        }

        private bool IsComponentInvalid(ComponentForm componentForm)
        {
            return isEmptyStringIncluded(componentForm.type, componentForm.name)
                   || isContainsWhitespace(componentForm.type)
                   || isContainsNotAllowedCharacters(componentForm.type)
                   || componentForm.amount < 0
                   || componentForm.price < 0;
        }

        private bool IsProductInvalid(ProductForm productForm)
        {
            return isEmptyStringIncluded(productForm.type, productForm.name, productForm.description)
                   || isContainsNotAllowedCharacters(productForm.type)
                   || isContainsWhitespace(productForm.type)
                   || productForm.price < 0;
        }


        private bool isEmptyStringIncluded(params string[] strings)
        {
            return strings.Any(string.IsNullOrEmpty);
        }

        private bool isContainsWhitespace(params string[] strings)
        {
            return strings.Any(str => str.Any(char.IsWhiteSpace));
        }

        private bool isContainsNotAllowedCharacters(params string[] strings)
        {
            return !strings.Any(str => Regex.IsMatch(str, "\\w+"));
        }


//        public void validateOrderForm(OrderForm orderForm)
//        {
//            if (isOrderInvalid(orderForm))
//            {
//                throw new InputMismatchException("Some component details are missing or invalid.");
//            }
//        }
//
//        public void validateCreditCardForm(CreditCardForm creditCardForm)
//        {
//            if (isCreditCardInvalid(creditCardForm))
//            {
//                throw new InputMismatchException("Some component details are missing or invalid.");
//            }
//        }
//
//        /*********************************
//         * Private Functions
//         *********************************/
//
//
//
//        private boolean isOrderInvalid(OrderForm orderForm)
//        {
//            String[] componentsTypes = orderForm.getComponentsTypes().toArray(new String[0]);
//            return isEmptyStringIncluded(orderForm.getProductType())
//                   || isEmptyStringIncluded(componentsTypes);
//        }
//
//        private boolean isCreditCardInvalid(CreditCardForm creditCardForm)
//        {
//            return isEmptyStringIncluded(creditCardForm.getCreditNumber(), creditCardForm.getExpireDate(),
//                creditCardForm.getCvv());
//        }
//
    }
}