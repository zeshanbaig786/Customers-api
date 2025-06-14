using Customers.Api.Domain;

namespace Customers.Api.DTOs;

public class CustomerReadDTO
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public PhoneNumberDTO PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public AddressDTO Address { get; set; }
    public string CustomerType { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string Status { get; set; } = "Active";
    public string? Notes { get; set; }

    public static CustomerReadDTO From(Customer customer)
    {
        ArgumentNullException.ThrowIfNull(customer);
        return new CustomerReadDTO
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            MiddleName = customer.MiddleName,
            LastName = customer.LastName,
            EmailAddress = customer.EmailAddress,
            PhoneNumber = PhoneNumberDTO.From(customer.PhoneNumber),
            DateOfBirth = customer.DateOfBirth,
            Address = AddressDTO.From(customer.Address),
            CustomerType = customer.CustomerType,
            CreatedAt = customer.CreatedAt,
            UpdatedAt = customer.UpdatedAt,
            Status = customer.Status,
            Notes = customer.Notes
        };
    }
}
