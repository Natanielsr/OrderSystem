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
            passwordService.HashPassword("password123")
        );

        user.SetDefaultEntityProps();

        context.Users.Add(user);
        context.SaveChanges();
        Console.WriteLine("User Test created in database.");
    }
}
