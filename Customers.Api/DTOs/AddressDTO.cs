using System.ComponentModel.DataAnnotations;

namespace Customers.Api.DTOs;

public class AddressDTO
{
    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Street { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string City { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string State { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid postal code format.")]
    public string PostalCode { get; set; } = string.Empty;
}