using Customers.Api.Abstractions;
using Customers.Api.DTOs;
using Customers.Api.Models;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Customers.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController(ILogger<CustomerController> logger,
    ICustomerService customerService) : ControllerBase
{
    // GET: api/customer
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CustomerReadDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<CustomerReadDTO>>> GetAllCustomers()
    {
        Result<List<CustomerReadDTO>> result = await customerService.GetAllAsync();
        if (result.IsFailed)
        {
            logger.LogError("Failed to retrieve all customers: {Error}", result.Errors);
            return BadRequest(ErrorResponse.FromResult(result));
        }
        logger.LogInformation("Successfully retrieved {Count} customers.", result.Value.Count);
        return Ok(result.Value);
    }

    // GET: api/customer/{id}
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CustomerReadDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerReadDTO>> GetCustomerById(Guid id)
    {
        Result<CustomerReadDTO?> result = await customerService.GetByIdAsync(id);
        if (result.IsFailed)
        {
            logger.LogError("Failed to retrieve customer with ID {Id}: {Error}", id, result.Errors);
            return BadRequest(ErrorResponse.FromResult(result));
        }
        if (result.Value is null)
        {
            logger.LogWarning("Customer with ID {Id} not found.", id);
            return NotFound($"Customer with ID {id} not found.");
        }
        logger.LogInformation("Successfully retrieved customer with ID {Id}.", id);
        return Ok(result.Value);
    }

    // POST: api/customer
    [HttpPost]
    [ProducesResponseType(typeof(CustomerReadDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CustomerReadDTO>> CreateCustomer([FromBody] CustomerCreateDTO dto)
    {
        if (!ModelState.IsValid)
        {
            logger.LogWarning("Invalid model state for customer creation: {Errors}", ModelState.Values.SelectMany(v => v.Errors));
            return BadRequest(ModelState);
        }
        Result<CustomerReadDTO> result = await customerService.CreateAsync(dto);
        if (result.IsFailed)
        {
            logger.LogError("Failed to create customer: {Errors}", result.Errors);
            return BadRequest(ErrorResponse.FromResult(result));
        }
        logger.LogInformation("Successfully created customer with ID {Id}.", result.Value.Id);
        
        return CreatedAtAction(nameof(GetCustomerById), new { id = result.Value.Id }, null);
    }

    // PUT: api/customer/{id}
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCustomer(Guid id, [FromBody] CustomerUpdateDTO dto)
    {
        if (!ModelState.IsValid)
        {
            logger.LogWarning("Invalid model state for customer update: {Errors}", ModelState.Values.SelectMany(v => v.Errors));
            return BadRequest(ModelState);
        }
        Result<bool> result = await customerService.UpdateAsync(id, dto);
        if (result.IsFailed)
        {
            logger.LogError("Failed to update customer with ID {Id}: {Errors}", id, result.Errors);
            return BadRequest(ErrorResponse.FromResult(result));
        }
        logger.LogInformation("Successfully updated customer with ID {Id}.", id);
        return NoContent();
    }
    
    // DELETE: api/customer/{id}
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCustomer(Guid id)
    {
        Result<bool> result = await customerService.DeleteAsync(id);
        if (result.IsFailed)
        {
            logger.LogError("Failed to delete customer with ID {Id}: {Errors}", id, result.Errors);
            return BadRequest(ErrorResponse.FromResult(result));
        }
        if (!result.Value)
        {
            logger.LogWarning("Customer with ID {Id} not found for deletion.", id);
            return NotFound($"Customer with ID {id} not found.");
        }
        logger.LogInformation("Successfully deleted customer with ID {Id}.", id);
        return NoContent();
    }

    // PATCH: api/customer/{id}/address
    [HttpPatch("{id:guid}/address")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCustomerAddress(Guid id,
        [FromBody] CustomerUpdateAddressDTO dto)
    {
        if (!ModelState.IsValid)
        {
            logger.LogWarning("Invalid model state for customer address update: {Errors}", ModelState.Values.SelectMany(v => v.Errors));
            return BadRequest(ModelState);
        }
        Result<bool> result = await customerService.UpdateAddressAsync(id, dto);
        if (result.IsFailed)
        {
            logger.LogError("Failed to update address for customer with ID {Id}: {Errors}", id, result.Errors);
            return BadRequest(ErrorResponse.FromResult(result));
        }
        if (!result.Value)
        {
            logger.LogWarning("Customer with ID {Id} not found for address update.", id);
            return NotFound($"Customer with ID {id} not found.");
        }
        logger.LogInformation("Successfully updated address for customer with ID {Id}.", id);
        return NoContent();
    }

    // PATCH: api/customer/{id}/phonenumber
    [HttpPatch("{id:guid}/phonenumber")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCustomerPhoneNumber(Guid id,
        [FromBody] CustomerUpdatePhoneNumberDTO dto)
    {
        if (!ModelState.IsValid)
        {
            logger.LogWarning("Invalid model state for customer phone number update: {Errors}", ModelState.Values.SelectMany(v => v.Errors));
            return BadRequest(ModelState);
        }
        Result<bool> result = await customerService.UpdatePhoneNumberAsync(id, dto);
        if (result.IsFailed)
        {
            logger.LogError("Failed to update phone number for customer with ID {Id}: {Errors}", id, result.Errors);
            return BadRequest(ErrorResponse.FromResult(result));
        }
        if (!result.Value)
        {
            logger.LogWarning("Customer with ID {Id} not found for phone number update.", id);
            return NotFound($"Customer with ID {id} not found.");
        }
        logger.LogInformation("Successfully updated phone number for customer with ID {Id}.", id);
        return NoContent();
    }

    // PATCH: api/customer/{id}/status
    [HttpPatch("{id:guid}/status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCustomerStatus(Guid id, [FromBody] CustomerUpdateStatusDTO dto)
    {
        if (!ModelState.IsValid)
        {
            logger.LogWarning("Invalid model state for customer status update: {Errors}", ModelState.Values.SelectMany(v => v.Errors));
            return BadRequest(ModelState);
        }
        Result<bool> result = await customerService.UpdateStatusAsync(id, dto);
        if (result.IsFailed)
        {
            logger.LogError("Failed to update status for customer with ID {Id}: {Errors}", id, result.Errors);
            return BadRequest(ErrorResponse.FromResult(result));
        }
        if (!result.Value)
        {
            logger.LogWarning("Customer with ID {Id} not found for status update.", id);
            return NotFound($"Customer with ID {id} not found.");
        }
        logger.LogInformation("Successfully updated status for customer with ID {Id}.", id);
        return NoContent();
    }
}
