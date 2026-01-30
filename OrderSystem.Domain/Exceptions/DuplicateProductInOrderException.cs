using System;

namespace OrderSystem.Domain.Exceptions;

public class DuplicateProductInOrderException : Exception
{
    public DuplicateProductInOrderException() : base("Duplicate Product In Order") { }
}
