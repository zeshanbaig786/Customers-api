using System.ComponentModel.DataAnnotations;
using Customers.Api.Domain;

namespace Customers.Api.DTOs;

public class PhoneNumberDTO
{
    [Required]
    [RegularExpression(@"^\+\d{1,3}$", ErrorMessage = "Invalid country code format.")]
    [StringLength(5, MinimumLength = 1, ErrorMessage = "Country code must be between 1 and 5 characters.")]
    public string CountryCode { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"^\d{3}$", ErrorMessage = "Invalid area code format.")]
    [StringLength(3, MinimumLength = 3, ErrorMessage = "Area code must be exactly 3 digits.")]
    public string AreaCode { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"^\d{7}$", ErrorMessage = "Invalid phone number format.")]
    [StringLength(7, MinimumLength = 7, ErrorMessage = "Phone number must be exactly 7 digits.")]
    public string Number { get; set; } = string.Empty;

    public static PhoneNumberDTO From(PhoneNumber customerPhoneNumber)
    {
        ArgumentNullException.ThrowIfNull(customerPhoneNumber);
        return new PhoneNumberDTO
        {
            CountryCode = customerPhoneNumber.CountryCode,
            AreaCode = customerPhoneNumber.AreaCode,
            Number = customerPhoneNumber.Number
        };
    }
}
