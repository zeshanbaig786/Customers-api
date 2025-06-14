using System.ComponentModel.DataAnnotations;

namespace Customers.Api.Domain;

public class PhoneNumber(string countryCode, string areaCode, string number) : BaseEntity
{
    [Required] // Ensures country code is provided
    [RegularExpression(@"^\+\d{1,3}$", ErrorMessage = "Invalid country code format.")] // Validates country code format
    [StringLength(5, MinimumLength = 1, ErrorMessage = "Country code must be between 1 and 5 characters.")]
    public string CountryCode { get; set; } = countryCode; // e.g., +1
    [Required] // Ensures area code is provided
    [RegularExpression(@"^\d{3}$", ErrorMessage = "Invalid area code format.")] // Validates area code format
    [StringLength(3, MinimumLength = 3, ErrorMessage = "Area code must be exactly 3 digits.")]
    public string AreaCode { get; set; } = areaCode; // e.g., 415
    [Required] // Ensures phone number is provided
    [RegularExpression(@"^\d{7}$", ErrorMessage = "Invalid phone number format.")] // Validates phone number format
    [StringLength(7, MinimumLength = 7, ErrorMessage = "Phone number must be exactly 7 digits.")]
    public string Number { get; set; } = number; // e.g., 5551234

    public override string ToString()
    {
        return $"{CountryCode}-{AreaCode}-{Number}";
    }
}
