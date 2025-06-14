using Customers.Api.DTOs;

namespace Customers.Api.Abstractions;

public interface ICustomerService
{
    // Query
    Task<IEnumerable<CustomerReadDTO>> GetAllAsync();
    Task<CustomerReadDTO?> GetByIdAsync(Guid id);

    // Commands
    Task<CustomerReadDTO> CreateAsync(CustomerCreateDTO dto);
    Task<bool> UpdateAsync(Guid id, CustomerUpdateDTO dto);
    Task<bool> PatchAsync(Guid id, CustomerPatchDTO dto);
    Task<bool> DeleteAsync(Guid id);

    // Sub-attribute updates
    Task<bool> UpdateAddressAsync(Guid id, CustomerUpdateAddressDTO dto);
    Task<bool> UpdatePhoneNumberAsync(Guid id, CustomerUpdatePhoneNumberDTO dto);
    Task<bool> UpdateStatusAsync(Guid id, CustomerUpdateStatusDTO dto);
}