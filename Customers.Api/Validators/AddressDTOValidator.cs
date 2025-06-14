using Customers.Api.DTOs;
using FluentValidation;

namespace Customers.Api.Validators;

public class AddressDTOValidator: AbstractValidator<AddressDTO>
{
    public AddressDTOValidator()
    {
        RuleFor(a => a.Street)
            .NotEmpty().WithMessage("Street is required.")
            .Length(1, 200).WithMessage("Street must be between 1 and 200 characters.");
        
        RuleFor(a => a.City)
            .NotEmpty().WithMessage("City is required.")
            .Length(1, 100).WithMessage("City must be between 1 and 100 characters.");
        
        RuleFor(a => a.State)
            .NotEmpty().WithMessage("State is required.")
            .Length(1, 100).WithMessage("State must be between 1 and 100 characters.");
        
        RuleFor(a => a.PostalCode)
            .NotEmpty().WithMessage("Postal code is required.")
            .Length(1, 20).WithMessage("Postal code must be between 1 and 20 characters.");
    }
}
