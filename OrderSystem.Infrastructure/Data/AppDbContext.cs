using Microsoft.EntityFrameworkCore;
using OrderSystem.Domain.Entities;

namespace OrderSystem.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Use GUIDs fixos para que não mudem a cada execução
        var userId = Guid.Parse("d268846c-76d3-4556-9d22-19e48f4989e2");
        var p1Id = Guid.Parse("868d8763-125c-4384-8149-c12e52e50529");
        var p2Id = Guid.Parse("671b4025-a134-45e0-9e67-0c7f1a308967");

        modelBuilder.Entity<User>().HasData(
            new User(userId) { Name = "UserTest", Email = "usertest@email.com" }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product(p1Id, "Product1", 10m, 10),
            new Product(p2Id, "Product2", 20m, 20)
        );

    }

}
