using OrderSystem.Domain.Exceptions;

namespace OrderSystem.Domain.Entities;

public class Product : Entity
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public int AvailableQuantity { get; set; }

    public int ReduceInStock(int Quantity)
    {
        if (Quantity <= 0)
        {
            throw new Exception("Quantity must be bigger then zero");
        }

        if (Quantity > AvailableQuantity)
        {
            throw new QuantityProductInStockOverflowException();
        }


        this.AvailableQuantity -= Quantity;

        return this.AvailableQuantity;
    }
}
