using OrderSystem.Domain.Entities;
using OrderSystem.Domain.Services;

namespace OrderSystem.Infrastructure.Data;

public static class DbInitializer
{
    public static void Seed(AppDbContext context, IPasswordService passwordService)
    {
        context.Database.EnsureCreated();

        if (context.Users.Any()) return; // Banco já tem dados

        var user = new User(
            Guid.NewGuid(),
            "UserTest",
            "usertest@email.com",
            passwordService.HashPassword("password123"),
            UserRole.User
        );
        user.SetDefaultEntityProps();

        var admin = new User(
            Guid.NewGuid(),
            "admin",
            "admin@email.com",
            passwordService.HashPassword("admin"),
            UserRole.Admin
        );
        admin.SetDefaultEntityProps();


        context.Users.Add(user);
        context.Users.Add(admin);
        context.SaveChanges();
        Console.WriteLine("UserTest and admin created in database.");
    }
}
