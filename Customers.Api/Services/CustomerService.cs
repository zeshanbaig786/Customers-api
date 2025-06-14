using Customers.Api.Abstractions;
using Customers.Api.DTOs;

namespace Customers.Api.Services;

public class CustomerService(ILogger<CustomerService> logger, AppDatabaseContext database) : ICustomerService
{
    public Task<IEnumerable<CustomerReadDTO>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CustomerReadDTO?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<CustomerReadDTO> CreateAsync(CustomerCreateDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Guid id, CustomerUpdateDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> PatchAsync(Guid id, CustomerPatchDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAddressAsync(Guid id, CustomerUpdateAddressDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdatePhoneNumberAsync(Guid id, CustomerUpdatePhoneNumberDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateStatusAsync(Guid id, CustomerUpdateStatusDTO dto)
    {
        throw new NotImplementedException();
    }
}