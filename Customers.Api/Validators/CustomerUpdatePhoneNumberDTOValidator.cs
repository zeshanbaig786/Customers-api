using Customers.Api.DTOs;
using FluentValidation;
using FluentValidation.Validators;

namespace Customers.Api.Validators;

public class CustomerUpdatePhoneNumberDTOValidator : AbstractValidator<CustomerUpdatePhoneNumberDTO>
{
    public CustomerUpdatePhoneNumberDTOValidator()
    {
        RuleFor(c => c.PhoneNumber)
            .NotNull().WithMessage("Phone number is required.")
            .SetValidator(new PhoneNumberDTOValidator()); // Assuming PhoneNumberDTOValidator is defined elsewhere
    }
}
