using System.ComponentModel.DataAnnotations;
using Customers.Api.Domain;

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
    public string PostalCode { get; set; } = string.Empty;

    public static AddressDTO From(Address customerAddress)
    {
        ArgumentNullException.ThrowIfNull(customerAddress);
        return new AddressDTO
        {
            Street = customerAddress.Street,
            City = customerAddress.City,
            State = customerAddress.State,
            PostalCode = customerAddress.PostalCode
        };
    }

    public void UpdateTo(Address customerAddress)
    {
        ArgumentNullException.ThrowIfNull(customerAddress);
        customerAddress.Street = Street;
        customerAddress.City = City;
        customerAddress.State = State;
        customerAddress.PostalCode = PostalCode;
    }
}
