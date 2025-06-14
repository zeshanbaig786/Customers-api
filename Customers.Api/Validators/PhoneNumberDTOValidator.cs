using Customers.Api.DTOs;
using FluentValidation;

namespace Customers.Api.Validators;

public class PhoneNumberDTOValidator : AbstractValidator<PhoneNumberDTO>
{
    public PhoneNumberDTOValidator()
    {
        RuleFor(p => p.CountryCode)
            .NotEmpty().WithMessage("Country code is required.")
            .Length(1, 5).WithMessage("Country code must be between 1 and 5 characters.")
            .Matches(@"^\+\d{1,3}$").WithMessage("Invalid country code format.");
        RuleFor(p => p.AreaCode)
            .NotEmpty().WithMessage("Area code is required.")
            .Length(3).WithMessage("Area code must be exactly 3 digits.")
            .Matches(@"^\d{3}$").WithMessage("Invalid area code format.");
        RuleFor(p => p.Number)
            .NotEmpty().WithMessage("Phone number is required.")
            .Length(7).WithMessage("Phone number must be exactly 7 digits.")
            .Matches(@"^\d{7}$").WithMessage("Invalid phone number format.");
    }
}
