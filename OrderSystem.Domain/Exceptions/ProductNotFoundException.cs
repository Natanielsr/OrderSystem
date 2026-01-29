using System;

namespace OrderSystem.Domain.Exceptions;

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException() : base("Product Id in order doesn't exist") { }
}
