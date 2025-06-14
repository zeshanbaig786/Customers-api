using Customers.Api.DTOs;
using FluentResults;

namespace Customers.Api.Abstractions;

public interface ICustomerService
{
    // Query
    Task<Result<List<CustomerReadDTO>>> GetAllAsync();
    Task<Result<CustomerReadDTO?>> GetByIdAsync(Guid id);

    // Commands
    Task<Result<CustomerReadDTO>> CreateAsync(CustomerCreateDTO dto);
    Task<Result<bool>> UpdateAsync(Guid id, CustomerUpdateDTO dto);
    Task<Result<bool>> PatchAsync(Guid id, CustomerPatchDTO dto);
    Task<Result<bool>> DeleteAsync(Guid id);

    // Sub-attribute updates
    Task<Result<bool>> UpdateAddressAsync(Guid id, CustomerUpdateAddressDTO dto);
    Task<Result<bool>> UpdatePhoneNumberAsync(Guid id, CustomerUpdatePhoneNumberDTO dto);
    Task<Result<bool>> UpdateStatusAsync(Guid id, CustomerUpdateStatusDTO dto);
}
