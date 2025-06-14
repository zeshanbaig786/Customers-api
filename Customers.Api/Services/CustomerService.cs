using Customers.Api.Abstractions;
using Customers.Api.Data;
using Customers.Api.DTOs;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Customers.Api.Services;

public class CustomerService(ILogger<CustomerService> logger, AppDatabaseContext database) : ICustomerService
{
    public async Task<Result<List<CustomerReadDTO>>> GetAllAsync()
    {
        logger.LogInformation("Retrieving all customers from the database.");
        try
        {
            List<CustomerReadDTO> customerReadDtos = await database.Customers
                .Select(s => CustomerReadDTO.From(s))
                .ToListAsync();
            logger.LogInformation("Successfully retrieved {Count} customers.", customerReadDtos.Count);
            return Result.Ok(customerReadDtos);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while retrieving all customers.");
            return Result.Fail("");
        }
    }

    public Task<Result<CustomerReadDTO?>> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<CustomerReadDTO>> CreateAsync(CustomerCreateDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> UpdateAsync(Guid id, CustomerUpdateDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> PatchAsync(Guid id, CustomerPatchDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> UpdateAddressAsync(Guid id, CustomerUpdateAddressDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> UpdatePhoneNumberAsync(Guid id, CustomerUpdatePhoneNumberDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> UpdateStatusAsync(Guid id, CustomerUpdateStatusDTO dto)
    {
        throw new NotImplementedException();
    }
}
