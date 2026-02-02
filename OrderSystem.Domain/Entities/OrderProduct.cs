using System;

namespace OrderSystem.Domain.Entities;

public class OrderProduct : Entity
{
    public Guid OrderId { get; set; }
    public Order? Order { get; set; }
    public Guid ProductId { get; set; }
    public Product? ProductReference { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }

    public decimal Total
    {
        get
        {
            return UnitPrice * Quantity;
        }
    }
}
