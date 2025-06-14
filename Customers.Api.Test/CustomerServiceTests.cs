using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Customers.Api.Abstractions;
using Customers.Api.Data;
using Customers.Api.Domain;
using Customers.Api.DTOs;
using Customers.Api.Services;
using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Customers.Api.Test;

public class CustomerServiceTests
{
    private readonly Mock<ILogger<CustomerService>> _logger = new();
    private readonly Mock<IValidator<CustomerCreateDTO>> _createValidator = new();
    private readonly Mock<IValidator<CustomerUpdateDTO>> _updateValidator = new();
    private readonly Mock<IValidator<CustomerUpdateAddressDTO>> _updateAddressValidator = new();
    private readonly Mock<IValidator<CustomerUpdatePhoneNumberDTO>> _updatePhoneNumberValidator = new();

    private AppDatabaseContext GetInMemoryDbContext()
    {
        DbContextOptions<AppDatabaseContext> options = new DbContextOptionsBuilder<AppDatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new AppDatabaseContext(options);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsCustomers()
    {
        // Arrange
        AppDatabaseContext db = GetInMemoryDbContext();
        db.Customers.Add(new Customer { Id = Guid.NewGuid(), EmailAddress = "test@example.com" });
        await db.SaveChangesAsync();

        var service = new CustomerService(
            _logger.Object,
            _createValidator.Object,
            _updateValidator.Object,
            _updateAddressValidator.Object,
            _updatePhoneNumberValidator.Object,
            db
        );

        // Act
        Result<List<CustomerReadDTO>> result = await service.GetAllAsync();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Single(result.Value);
    }

    [Fact]
    public async Task CreateAsync_InvalidValidation_ReturnsFail()
    {
        // Arrange
        AppDatabaseContext db = GetInMemoryDbContext();
        var dto = new CustomerCreateDTO { EmailAddress = "invalid" };
        _createValidator.Setup(v => v.ValidateAsync(dto, default))
            .ReturnsAsync(new ValidationResult(new[] { new ValidationFailure("EmailAddress", "Invalid") }));

        var service = new CustomerService(
            _logger.Object,
            _createValidator.Object,
            _updateValidator.Object,
            _updateAddressValidator.Object,
            _updatePhoneNumberValidator.Object,
            db
        );

        // Act
        Result<CustomerReadDTO> result = await service.CreateAsync(dto);

        // Assert
        Assert.True(result.IsFailed);
    }
}
