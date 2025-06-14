using Customers.Api.DTOs;
using FluentValidation;

namespace Customers.Api.Validators;

public class CustomerUpdateAddressDTOValidator : AbstractValidator<CustomerUpdateAddressDTO>
{
    public CustomerUpdateAddressDTOValidator()
    {
        RuleFor(c => c.Address)
            .NotNull().WithMessage("Address is required.")
            .SetValidator(new AddressDTOValidator()); // Assuming AddressDTOValidator is defined elsewhere
    }
}
