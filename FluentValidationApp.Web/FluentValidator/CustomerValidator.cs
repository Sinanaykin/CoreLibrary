using FluentValidation;
using FluentValidationApp.Web.Models;

namespace FluentValidationApp.Web.FluentValidator
{
    public class CustomerValidator:AbstractValidator<Customer>
    {

        public CustomerValidator()
        {
            RuleFor(X=>X.Name).NotEmpty().WithMessage("NAME ALANI BOŞ GEÇİLEMEZ.");
            RuleFor(X => X.Email).EmailAddress().WithMessage("Email doğru format da değil");
        }

    }
}
