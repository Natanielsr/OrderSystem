using System;
using OrderSystem.Domain.Services;

namespace OrderSystem.Domain.Entities;

public static class UserRole
{
    public const string User = "User";
    public const string Admin = "Admin";
}

public class User : Entity
{
    public string? Username { get; private set; }
    public string? Email { get; private set; }
    public string? HashedPassword { get; private set; }
    private IPasswordService? _passwordService;

    public string? Role { get; private set; }

    protected User() { }

    public User(Guid id,
        string username,
        string email,
        string hashedPassword

        ) : base(id)
    {
        this.Username = username;
        this.Email = email;
        this.HashedPassword = hashedPassword;
        this.Role = UserRole.User;
    }

    public User(Guid id,
        string username,
        string email,
        string hashedPassword,
        string role

        ) : base(id)
    {
        this.Username = username;
        this.Email = email;
        this.HashedPassword = hashedPassword;
        this.Role = role;
    }

    public User(Guid id,
        DateTimeOffset creationDate,
        DateTimeOffset updateDate,
        bool active,
        string username,
        string email,
        string HashedPassword,
        string role

        ) : base(id, creationDate, updateDate, active)
    {
        this.Username = username;
        this.Email = email;
        this.HashedPassword = HashedPassword;
        this.Role = role;
    }

    public void SetNormalUserRole()
    {
        Role = UserRole.User;
    }

    public void SetPasswordService(IPasswordService passwordService)
    {
        _passwordService = passwordService;
    }

    public string HashPassword(string password)
    {
        if (_passwordService != null)
            HashedPassword = _passwordService.HashPassword(password);
        else
            throw new Exception("IPasswordService not set use SetPasswordService() method");

        return HashedPassword;
    }
}
