using System.ComponentModel.DataAnnotations;

namespace Customers.Api.DTOs;

public class CustomerUpdateStatusDTO
{
    [Required]
    [StringLength(50, MinimumLength = 1)]
    [RegularExpression(@"^(Active|Inactive|Suspended)$", ErrorMessage = "Status must be Active, Inactive, or Suspended.")]
    public string Status { get; set; } = "Active";
}