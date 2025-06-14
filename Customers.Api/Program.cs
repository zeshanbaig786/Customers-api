using System;
using Customers.Api.Abstractions;
using Customers.Api.Data;
using Customers.Api.Services;
using Customers.Api.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Logging.ClearProviders();
builder.Host.UseSerilog((ctx, lc) =>
{
    lc.Enrich.FromLogContext()
        .WriteTo.Console()
        .ReadFrom.Configuration(ctx.Configuration);
});
builder.Services.AddDbContext<AppDatabaseContext>(options =>
    options.UseSqlite("Data Source=customers.db"));
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddValidatorsFromAssemblyContaining<CustomerCreateDtoValidator>();

builder.Services.AddScoped<ICustomerService, CustomerService>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
