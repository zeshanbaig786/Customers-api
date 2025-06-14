using Customers.Api.Abstractions;
using Customers.Api.Data;
using Customers.Api.Domain;
using Customers.Api.DTOs;
using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace Customers.Api.Services;

public class CustomerService(ILogger<CustomerService> logger, 
    IValidator<CustomerCreateDTO> createValidator,
    AppDatabaseContext database) : ICustomerService
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

    public async Task<Result<CustomerReadDTO?>> GetByIdAsync(Guid id)
    {
        logger.LogInformation("Retrieving customer with ID {Id} from the database.", id);
        try
        {
            CustomerReadDTO? customer = await database.Customers
                .Where(c => c.Id == id)
                .Select(c => CustomerReadDTO.From(c))
                .SingleOrDefaultAsync();

            if (customer is null)
            {
                logger.LogWarning("Customer with ID {Id} not found.", id);
                return Result.Fail<CustomerReadDTO?>($"Customer with ID {id} not found.");
            }
            logger.LogInformation("Successfully retrieved customer with ID {Id}.", id);
            return customer;
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while retrieving customer with ID {Id}.", id);
            return Result.Fail<CustomerReadDTO?>(e.Message);
        }
    }

    public async Task<Result<CustomerReadDTO>> CreateAsync(CustomerCreateDTO dto)
    {
        logger.LogInformation("Creating a new customer with data: {@CustomerCreateDTO}", dto);
        try
        {
            ValidationResult validationResult = await createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                logger.LogWarning("Validation failed for customer creation: {Errors}", validationResult.Errors);
                return Result.Fail<CustomerReadDTO>(
                    validationResult.Errors.Select(e => e.ErrorMessage)
                        .ToList());
            }

            // check if email already exists
            if (await database.Customers.AnyAsync(c => c.EmailAddress == dto.EmailAddress))
            {
                logger.LogWarning("Customer with email {Email} already exists.", dto.EmailAddress);
                return Result.Fail<CustomerReadDTO>($"Customer with email {dto.EmailAddress} already exists.");
            }

            var customer = dto.ToCustomer();
            database.Customers.Add(customer);
            await database.SaveChangesAsync();
            var customerReadDto = CustomerReadDTO.From(customer);
            logger.LogInformation("Successfully created customer with ID {Id}.", customer.Id);
            return Result.Ok(customerReadDto);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while creating a new customer.");
            return Result.Fail<CustomerReadDTO>("An error occurred while creating a new customer");
        }
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
