using System.ComponentModel.DataAnnotations;
using OrderSystem.Domain.Exceptions;

namespace OrderSystem.Domain.Entities;

public class Product : Entity
{
    public string Name { get; private set; } = string.Empty;
    public decimal Price { get; private set; }

    public int AvailableQuantity { get; private set; }

    public string ImagePath { get; set; } = string.Empty;

    public int Version { get; set; }

    protected Product() { }

    public Product(
        Guid id,
        string name,
        decimal price,
        int availableQuantity
        ) : base(id)
    {
        this.Name = name;
        this.Price = price;
        this.AvailableQuantity = availableQuantity;
        SetActive();
    }

    public Product(
        Guid id,
        DateTimeOffset creationDate,
        DateTimeOffset updateDate,
        bool active,
        string name,
        decimal price,
        int availableQuantity
        ) : base(id, creationDate, updateDate, active)
    {
        this.Name = name;
        this.Price = price;
        this.AvailableQuantity = availableQuantity;
    }

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
