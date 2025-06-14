using System.ComponentModel.DataAnnotations;

namespace Customers.Api.Domain;

public class Address(string street, string city, string state, string postalCode) : BaseEntity
{
    [Required] // Ensures street address is provided
    [StringLength(200, MinimumLength = 1)] // Limits length of street address
    public string Street { get; set; } = street;

    [Required] // Ensures city is provided
    [StringLength(100, MinimumLength = 1)] // Limits length of city
    public string City { get; set; } = city;

    [Required] // Ensures state is provided
    [StringLength(100, MinimumLength = 1)] // Limits length of state
    public string State { get; set; } = state;

    [Required] // Ensures postal code is provided
    [StringLength(10, MinimumLength = 5, ErrorMessage = "Postal code must be between 5 and 10 characters.")]
    public string PostalCode { get; set; } = postalCode;
}
