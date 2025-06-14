using System.ComponentModel.DataAnnotations;

namespace Customers.Api.DTOs;

public class CustomerUpdateAddressDTO
{
    [Required]
    public AddressDTO Address { get; set; }
}