using System.ComponentModel.DataAnnotations;

namespace Customers.Api.Domain;

public class Address : BaseEntity
{
    [Required] // Ensures street address is provided
    [StringLength(200, MinimumLength = 1)] // Limits length of street address
    public string Street { get; set; } = string.Empty;

    [Required] // Ensures city is provided
    [StringLength(100, MinimumLength = 1)] // Limits length of city
    public string City { get; set; } = string.Empty;

    [Required] // Ensures state is provided
    [StringLength(100, MinimumLength = 1)] // Limits length of state
    public string State { get; set; } = string.Empty;

    [Required] // Ensures postal code is provided
    [RegularExpression(@"^\d{5}(-\d{4})?$",
        ErrorMessage = "Invalid postal code format.")] // Validates postal code format
    public string PostalCode { get; set; } = string.Empty;
}