using System;

namespace OrderSystem.Domain.Exceptions;

public class UsernameAlreadyExistsException : BadRequest
{
    public UsernameAlreadyExistsException() : base("Username Already exists")
    {
    }
}
