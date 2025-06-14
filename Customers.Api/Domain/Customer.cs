using System.ComponentModel.DataAnnotations;

namespace Customers.Api.Domain;

public class Customer: BaseEntity
{
    [Required] // Ensures first name is provided
    [StringLength(100, MinimumLength = 1)] // Limits length of first name
    public string FirstName { get; set; } = string.Empty;
    [StringLength(100, MinimumLength = 0)] // Allows middle name to be empty or null
    [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Middle name can only contain letters and spaces.")] // Validates middle name format
    public string? MiddleName { get; set; } // Nullable middle name
    [Required] // Ensures last name is provided
    [StringLength(100, MinimumLength = 1)] // Limits length of last name
    [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Last name can only contain letters and spaces.")] // Validates last name format
    public string LastName { get; set; } = string.Empty;
    [Required]
    [EmailAddress] // Ensures valid email format
    public string EmailAddress { get; set; } = string.Empty; // Unique email address
    [Required]
    public PhoneNumber PhoneNumber { get; set; }

    [Required] // Ensures date of birth is provided
    [DataType(DataType.Date)] // Specifies that this is a date type
    [Range(typeof(DateTime), "1900-01-01", "2100-12-31", ErrorMessage = "Date of birth must be between 1900 and 2100.")]
    public DateTime DateOfBirth { get; set; } // Date of birth
    [Required] // Ensures address is provided
    public Address Address { get; set; } = new Address(); // Customer's address
    [Required]
    [StringLength(20, MinimumLength = 1)] // Limits length of customer type
    [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Customer type can only contain letters and spaces.")] // Validates customer type format
    public string CustomerType { get; set; } = string.Empty; // Type of customer (e.g., Regular, Premium)
    [Required] // Ensures creation date is provided
    [DataType(DataType.DateTime)] // Specifies that this is a date-time type
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Creation date of the customer record
    [DataType(DataType.DateTime)] // Specifies that this is a date-time type
    public DateTime? UpdatedAt { get; set; } // Last update date of the customer record, nullable
    [Required]
    [StringLength(50, MinimumLength = 1)] // Limits length of status
    [RegularExpression(@"^(Active|Inactive|Suspended)$", ErrorMessage = "Status must be Active, Inactive, or Suspended.")] // Validates status format
    public string Status { get; set; } = "Active"; // Status of the customer (e.g., Active, Inactive, Suspended)
    [StringLength(500, MinimumLength = 0)] // Allows notes to be empty or null
    public string? Notes { get; set; } // Additional notes about the customer
}