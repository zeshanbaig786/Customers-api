using System.ComponentModel.DataAnnotations;

namespace Customers.Api.DTOs;

public class CustomerPatchDTO
{
    [StringLength(100, MinimumLength = 1)]
    public string? FirstName { get; set; }

    [StringLength(100, MinimumLength = 0)]
    [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Middle name can only contain letters and spaces.")]
    public string? MiddleName { get; set; }

    [StringLength(100, MinimumLength = 1)]
    [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Last name can only contain letters and spaces.")]
    public string? LastName { get; set; }

    [EmailAddress]
    public string? EmailAddress { get; set; }

    public PhoneNumberDTO? PhoneNumber { get; set; }

    [DataType(DataType.Date)]
    [Range(typeof(DateTime), "1900-01-01", "2100-12-31", ErrorMessage = "Date of birth must be between 1900 and 2100.")]
    public DateTime? DateOfBirth { get; set; }

    public AddressDTO? Address { get; set; }

    [StringLength(20, MinimumLength = 1)]
    [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Customer type can only contain letters and spaces.")]
    public string? CustomerType { get; set; }

    [StringLength(50, MinimumLength = 1)]
    [RegularExpression(@"^(Active|Inactive|Suspended)$", ErrorMessage = "Status must be Active, Inactive, or Suspended.")]
    public string? Status { get; set; }

    [StringLength(500, MinimumLength = 0)]
    public string? Notes { get; set; }
}