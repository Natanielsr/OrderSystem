using System;

namespace OrderSystem.Domain.Entities;

public class User : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public User(Guid id) : base(id)
    {
    }
}
