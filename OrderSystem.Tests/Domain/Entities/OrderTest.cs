using System;
using OrderSystem.Domain.Entities;

namespace OrderSystem.Tests.Domain.Entities;

public class OrderTest
{
    [Fact]
    public void TotalTest()
    {
        //Arrange
        OrderProduct orderProduct1 = createSimpleOrderProduct("Product1", 10.00m, 10);
        OrderProduct orderProduct2 = createSimpleOrderProduct("Product2", 10.00m, 10);

        Order order = createSimpleOrder();
        order.AddProductOrder(orderProduct1);
        order.AddProductOrder(orderProduct2);

        //Act
        var total = Order.CalcTotal(order.OrderProducts);

        //Assert
        Assert.Equal(200m, total);
    }

    public Order createSimpleOrder()
    {
        return new Order()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTimeOffset.UtcNow,
            UpdateDate = DateTimeOffset.UtcNow,
            Active = true,
            OrderProducts = new List<OrderProduct>(),
            UserId = Guid.Empty,
            UserName = "userName",
            UserEmail = "userEmail",
            Total = 0,
            Status = OrderStatus.Pending,
            Code = "code",
            AddressId = Guid.Empty
        };
    }

    public OrderProduct createSimpleOrderProduct(string name, decimal price, int quantity)
    {
        return new OrderProduct()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTimeOffset.UtcNow,
            UpdateDate = DateTimeOffset.UtcNow,
            Active = true,
            OrderId = Guid.Empty,
            ProductId = Guid.NewGuid(),
            ProductName = name,
            UnitPrice = price,
            Quantity = quantity
        };
    }
}
