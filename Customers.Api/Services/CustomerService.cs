using Customers.Api.Abstractions;
using Customers.Api.Data;
using Customers.Api.Domain;
using Customers.Api.DTOs;
using Customers.Api.Validators;
using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace Customers.Api.Services;

public class CustomerService(ILogger<CustomerService> logger,
    IValidator<CustomerCreateDTO> createValidator,
    IValidator<CustomerUpdateDTO> updateValidator,
    IValidator<CustomerUpdateAddressDTO> updateAddressValidator,
    IValidator<CustomerUpdatePhoneNumberDTO> updatePhoneNumberValidator,
    AppDatabaseContext database) : ICustomerService
{
    public async Task<Result<List<CustomerReadDTO>>> GetAllAsync()
    {
        logger.LogInformation("Retrieving all customers from the database.");
        try
        {
            List<CustomerReadDTO> customerReadDtos = await database.Customers
                .Include(d => d.PhoneNumber)
                .Include(d => d.Address)
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
                .Include(d => d.PhoneNumber)
                .Include(d => d.Address)
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

    public async Task<Result<bool>> UpdateAsync(Guid id, CustomerUpdateDTO dto)
    {
        try
        {
            logger.LogInformation("Updating customer with ID {Id} with data: {@CustomerUpdateDTO}", id, dto);

            ValidationResult validationResult = await updateValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                logger.LogWarning("Validation failed for customer update: {Errors}", validationResult.Errors);
                return Result.Fail<bool>(
                    validationResult.Errors.Select(e => e.ErrorMessage)
                        .ToList());
            }

            Customer? customer = await database.Customers
                .Include(d => d.PhoneNumber)
                .Include(d => d.Address)
                .SingleOrDefaultAsync(d => d.Id == id);
            if (customer is null)
            {
                logger.LogWarning("Customer with ID {Id} not found.", id);
                return Result.Fail<bool>($"Customer with ID {id} not found.");
            }

            logger.LogInformation("Found customer with ID {Id} for update.", id);


            dto.UpdateTo(customer);
            await database.SaveChangesAsync();

            return Result.Ok(true); // Placeholder for successful update
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while updating customer with ID {Id}.", id);
            return Result.Fail<bool>("An error occurred while updating the customer.");
        }
    }

    public async Task<Result<bool>> DeleteAsync(Guid id)
    {
        logger.LogInformation("Deleting customer with ID {Id}.", id);
        try
        {
            Customer? customer = await database.Customers
                .SingleOrDefaultAsync(d => d.Id == id);
            if (customer is null)
            {
                logger.LogWarning("Customer with ID {Id} not found.", id);
                return Result.Fail<bool>($"Customer with ID {id} not found.");
            }
            database.Customers.Remove(customer);
            await database.SaveChangesAsync();
            logger.LogInformation("Successfully deleted customer with ID {Id}.", id);
            return Result.Ok(true);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while deleting customer with ID {Id}.", id);
            return Result.Fail<bool>("An error occurred while deleting the customer.");
        }
    }

    public async Task<Result<bool>> UpdateAddressAsync(Guid id, CustomerUpdateAddressDTO dto)
    {
        logger.LogInformation("Updating address for customer with ID {Id} with data: {@CustomerUpdateAddressDTO}", id, dto);
        try
        {
            ValidationResult validationResult = await updateAddressValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                logger.LogWarning("Validation failed for customer address update: {Errors}", validationResult.Errors);
                return Result.Fail<bool>(
                    validationResult.Errors.Select(e => e.ErrorMessage)
                        .ToList());
            }
            Customer? customer = await database.Customers
                .Include(d => d.PhoneNumber)
                .Include(d => d.Address)
                .SingleOrDefaultAsync(d => d.Id == id);
            if (customer is null)
            {
                logger.LogWarning("Customer with ID {Id} not found.", id);
                return Result.Fail<bool>($"Customer with ID {id} not found.");
            }
            dto.Address.UpdateTo(customer.Address);
            await database.SaveChangesAsync();
            logger.LogInformation("Successfully updated address for customer with ID {Id}.", id);
            return Result.Ok(true);
        }
        catch (Exception e)
        {
            logger.LogError(e,
                "An error occurred while updating address for customer with ID {Id}.", id);
            return Result.Fail<bool>("An error occurred while updating the customer's address.");
        }
    }

    public async Task<Result<bool>> UpdatePhoneNumberAsync(Guid id, CustomerUpdatePhoneNumberDTO dto)
    {
        logger.LogInformation("Updating phone number for customer with ID {Id} with data: {@CustomerUpdateAddressDTO}", id, dto);
        try
        {
            ValidationResult validationResult = await updatePhoneNumberValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                logger.LogWarning("Validation failed for customer phone number update: {Errors}", validationResult.Errors);
                return Result.Fail<bool>(
                    validationResult.Errors.Select(e => e.ErrorMessage)
                        .ToList());
            }
            Customer? customer = await database.Customers
                .Include(d => d.PhoneNumber)
                .Include(d => d.Address)
                .SingleOrDefaultAsync(d => d.Id == id);
            if (customer is null)
            {
                logger.LogWarning("Customer with ID {Id} not found.", id);
                return Result.Fail<bool>($"Customer with ID {id} not found.");
            }
            dto.PhoneNumber.UpdateTo(customer.PhoneNumber);
            await database.SaveChangesAsync();
            logger.LogInformation("Successfully updated phone number for customer with ID {Id}.", id);
            return Result.Ok(true);
        }
        catch (Exception e)
        {
            logger.LogError(e,
                "An error occurred while updating phone number for customer with ID {Id}.", id);
            return Result.Fail<bool>("An error occurred while updating the customer's phone number.");
        }
    }

    public async Task<Result<bool>> UpdateStatusAsync(Guid id, CustomerUpdateStatusDTO dto)
    {
        logger.LogInformation("Updating status for customer with ID {Id} with data: {@CustomerUpdateStatusDTO}", id, dto);
        try
        {
            Customer? customer = await database.Customers
                .Include(d => d.PhoneNumber)
                .Include(d => d.Address)
                .SingleOrDefaultAsync(c => c.Id == id);
            if (customer is null)
            {
                logger.LogWarning("Customer with ID {Id} not found.", id);
                return Result.Fail<bool>($"Customer with ID {id} not found.");
            }
            customer.Status = dto.Status;
            await database.SaveChangesAsync();
            logger.LogInformation("Successfully updated status for customer with ID {Id}.", id);
            return Result.Ok(true);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while updating status for customer with ID {Id}.", id);
            return Result.Fail<bool>("An error occurred while updating the customer's status.");
        }
    }
}
