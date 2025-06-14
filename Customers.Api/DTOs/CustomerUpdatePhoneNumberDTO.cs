using System.ComponentModel.DataAnnotations;

namespace Customers.Api.DTOs;

public class CustomerUpdatePhoneNumberDTO
{
    [Required]
    public PhoneNumberDTO PhoneNumber { get; set; }
}