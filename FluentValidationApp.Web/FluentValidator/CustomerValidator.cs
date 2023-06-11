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

            RuleForEach(X => X.Address).SetValidator(new AddressValidator());
            RuleFor(x => x.Gender).IsInEnum().WithMessage(" erkek için 1 bayan  için 2 olmalıdır");
               
        }

    }
}
