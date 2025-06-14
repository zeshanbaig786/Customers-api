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
}