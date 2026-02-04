using System;

namespace OrderSystem.Domain.Exceptions;

public class EmailAlreadyExistsException : BadRequest
{
    public EmailAlreadyExistsException() : base("Email Already Exists")
    {
    }
}
