using System;
using OrderSystem.Domain.Services;

namespace OrderSystem.Domain.Entities;

public class User : Entity
{
    public string Username { get; private set; }
    public string Email { get; private set; }
    public string HashedPassword { get; private set; }
    private IPasswordService? _passwordService;

    public User(Guid id,
        string username,
        string email,
        string HashedPassword

        ) : base(id)
    {
        this.Username = username;
        this.Email = email;
        this.HashedPassword = HashedPassword;
    }

    public User(Guid id,
        DateTimeOffset creationDate,
        DateTimeOffset updateDate,
        bool active,
        string username,
        string email,
        string HashedPassword

        ) : base(id, creationDate, updateDate, active)
    {
        this.Username = username;
        this.Email = email;
        this.HashedPassword = HashedPassword;
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
