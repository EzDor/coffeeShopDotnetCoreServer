using System;
using System.Linq;
using System.Text.RegularExpressions;
using WebApplication.Controllers.Forms;
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


        /*********************************
        * Private Functions
        *********************************/


        private bool IsUserInvalid(UserForm userForm)
        {
            return isEmptyStringIncluded(userForm.username, userForm.password)
                   || isContainsWhitespace(userForm.username, userForm.password)
                   || isContainsNotAllowedCharacters(userForm.username);
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


//
//        public void validateProductForm(ProductForm productForm)
//        {
//            if (isProductInvalid(productForm))
//            {
//                throw new InputMismatchException("Some product details are missing or invalid.");
//            }
//        }
//
//        public void validateComponentForm(ComponentForm componentForm)
//        {
//            if (isComponentInvalid(componentForm))
//            {
//                throw new InputMismatchException("Some component details are missing or invalid.");
//            }
//        }
//
//
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
//        private boolean isProductInvalid(ProductForm productForm)
//        {
//            return isEmptyStringIncluded(productForm.getType(), productForm.getName(), productForm.getDescription())
//                   || isContainsNotAllowedCharacters(productForm.getType())
//                   || isContainsWhitespace(productForm.getType())
//                   || productForm.getStatus() == null
//                   || productForm.getPrice() < 0;
//        }
//
//        private boolean isComponentInvalid(ComponentForm componentForm)
//        {
//            return isEmptyStringIncluded(componentForm.getType(), componentForm.getName())
//                   || isContainsWhitespace(componentForm.getType())
//                   || isContainsNotAllowedCharacters(componentForm.getType())
//                   || componentForm.getStatus() == null
//                   || componentForm.getAmount() < 0
//                   || componentForm.getPrice() < 0;
//        }
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