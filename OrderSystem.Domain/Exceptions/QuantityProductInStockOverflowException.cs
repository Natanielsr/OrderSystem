using System;

namespace OrderSystem.Domain.Exceptions;

public class QuantityProductInStockOverflowException : Exception
{
    public QuantityProductInStockOverflowException() : base("quantity required greater than available")
    {

    }
}
