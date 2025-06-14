using Customers.Api.Abstractions;
using Customers.Api.DTOs;
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
        Result<List<CustomerReadDTO>> allCustomers = await customerService.GetAllAsync();
        if (allCustomers.IsFailed)
        {
            logger.LogError("Failed to retrieve all customers: {Error}", allCustomers.Errors);
            return StatusCode(StatusCodes.Status500InternalServerError,
                "An error occurred while retrieving customers.");
        }
        logger.LogInformation("Successfully retrieved {Count} customers.", allCustomers.Value.Count);
        return Ok(allCustomers.Value);
    }

    // GET: api/customer/{id}
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CustomerReadDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<CustomerReadDTO> GetCustomerById(Guid id)
    {
        // Implementation here
        return Ok();
    }

    // POST: api/customer
    [HttpPost]
    [ProducesResponseType(typeof(CustomerReadDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public ActionResult<CustomerReadDTO> CreateCustomer([FromBody] CustomerCreateDTO dto)
    {
        // Implementation here
        return CreatedAtAction(nameof(GetCustomerById), new { id = Guid.NewGuid() }, null);
    }

    // PUT: api/customer/{id}
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateCustomer(Guid id, [FromBody] CustomerUpdateDTO dto)
    {
        // Implementation here
        return NoContent();
    }

    // PATCH: api/customer/{id}
    [HttpPatch("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult PatchCustomer(Guid id, [FromBody] CustomerPatchDTO dto)
    {
        // Implementation here
        return NoContent();
    }

    // DELETE: api/customer/{id}
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteCustomer(Guid id)
    {
        // Implementation here
        return NoContent();
    }

    // PATCH: api/customer/{id}/address
    [HttpPatch("{id:guid}/address")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateCustomerAddress(Guid id, [FromBody] CustomerUpdateAddressDTO dto)
    {
        // Implementation here
        return NoContent();
    }

    // PATCH: api/customer/{id}/phonenumber
    [HttpPatch("{id:guid}/phonenumber")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateCustomerPhoneNumber(Guid id, [FromBody] CustomerUpdatePhoneNumberDTO dto)
    {
        // Implementation here
        return NoContent();
    }

    // PATCH: api/customer/{id}/status
    [HttpPatch("{id:guid}/status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateCustomerStatus(Guid id, [FromBody] CustomerUpdateStatusDTO dto)
    {
        // Implementation here
        return NoContent();
    }
}
