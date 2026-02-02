using System;

namespace OrderSystem.Domain.Entities;

public class OrderProduct : Entity
{
    public Guid OrderId { get; private set; }
    public Order? Order { get; set; }
    public Guid ProductId { get; private set; }
    public Product? Product { get; set; }
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }

    public OrderProduct(Guid productId, decimal unitPrice, int quantity)
    {
        ProductId = productId;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }


    public decimal Total
    {
        get
        {
            return UnitPrice * Quantity;
        }
    }
}
