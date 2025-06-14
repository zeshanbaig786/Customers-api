using Customers.Api.DTOs;
using FluentValidation;

namespace Customers.Api.Validators;

public class CustomerUpdateDtoValidator: AbstractValidator<CustomerUpdateDTO>
{
    public CustomerUpdateDtoValidator()
    {
        RuleFor(c => c.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .Length(1, 100).WithMessage("First name must be between 1 and 100 characters.");
        RuleFor(c => c.MiddleName)
            .Matches(@"^[a-zA-Z\s]*$").WithMessage("Middle name can only contain letters and spaces.")
            .Length(0, 100).WithMessage("Middle name must be up to 100 characters long.");
        RuleFor(c => c.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .Length(1, 100).WithMessage("Last name must be between 1 and 100 characters.")
            .Matches(@"^[a-zA-Z\s]*$").WithMessage("Last name can only contain letters and spaces.");
        RuleFor(c => c.EmailAddress)
            .NotEmpty().WithMessage("Email address is required.")
            .EmailAddress().WithMessage("Invalid email format.");
        RuleFor(c => c.PhoneNumber)
            .NotNull().WithMessage("Phone number is required.");
        RuleFor(c => c.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required.")
            .InclusiveBetween(
                new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                new DateTime(2100, 12, 31, 23, 59, 59, DateTimeKind.Utc))
            .WithMessage("Date of birth must be between 1900-01-01 and 2100-12-31.");
        RuleFor(c => c.Address)
            .NotNull().WithMessage("Address is required.");
        RuleFor(c => c.CustomerType)
            .NotEmpty().WithMessage("Customer type is required.")
            .Length(1, 20).WithMessage("Customer type must be between 1 and 20 characters.")
            .Matches(@"^[a-zA-Z\s]*$").WithMessage("Customer type can only contain letters and spaces.");
        RuleFor(c => c.Status)
            .NotEmpty().WithMessage("Status is required.")
            .Length(1, 50).WithMessage("Status must be between 1 and 50 characters.")
            .Matches(@"^(Active|Inactive|Suspended)$")
            .WithMessage("Status must be Active, Inactive, or Suspended.");
        RuleFor(c => c.Notes)
            .Length(0, 500).WithMessage("Notes must be up to 500 characters long.");
    }
}
