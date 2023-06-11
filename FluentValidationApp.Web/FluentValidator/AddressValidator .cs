using FluentValidation;
using FluentValidationApp.Web.Models;

namespace FluentValidationApp.Web.FluentValidator
{
    public class AddressValidator:AbstractValidator<Address>
    {

        public AddressValidator()
        {
            RuleFor(X=>X.Content).NotEmpty().WithMessage("Content ALANI BOŞ GEÇİLEMEZ.");
            RuleFor(X => X.PostCode).EmailAddress().WithMessage("Posta kodu ALANI BOŞ GEÇİLEMEZ.");
        }

    }
}
