using Customers.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace Customers.Api.Data;

public class AppDatabaseContext : DbContext
{
    public AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) : base(options) { }

    public DbSet<Customer> Customers { get; set; }
    //public DbSet<PhoneNumber> PhoneNumbers { get; set; }
    //public DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>()
            .HasIndex(c => c.EmailAddress)
            .IsUnique();
    }
}
