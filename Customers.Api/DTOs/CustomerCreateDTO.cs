using System.ComponentModel.DataAnnotations;

namespace Customers.Api.DTOs;

public class CustomerCreateDTO
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string FirstName { get; set; } = string.Empty;

    [StringLength(100, MinimumLength = 0)]
    [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Middle name can only contain letters and spaces.")]
    public string? MiddleName { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 1)]
    [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Last name can only contain letters and spaces.")]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string EmailAddress { get; set; } = string.Empty;

    [Required]
    public PhoneNumberDTO PhoneNumber { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [Range(typeof(DateTime), "1900-01-01", "2100-12-31", ErrorMessage = "Date of birth must be between 1900 and 2100.")]
    public DateTime DateOfBirth { get; set; }

    [Required]
    public AddressDTO Address { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 1)]
    [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Customer type can only contain letters and spaces.")]
    public string CustomerType { get; set; } = string.Empty;

    [Required]
    [StringLength(50, MinimumLength = 1)]
    [RegularExpression(@"^(Active|Inactive|Suspended)$", ErrorMessage = "Status must be Active, Inactive, or Suspended.")]
    public string Status { get; set; } = "Active";

    [StringLength(500, MinimumLength = 0)]
    public string? Notes { get; set; }
}