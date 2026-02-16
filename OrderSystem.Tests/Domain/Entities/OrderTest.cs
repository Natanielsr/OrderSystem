using System;
using OrderSystem.Domain.Entities;

namespace OrderSystem.Tests.Domain.Entities;

public class OrderTest
{
    [Fact]
    public void TotalTest()
    {
        //Arrange
        OrderProduct orderProduct1 = new(Guid.NewGuid(), "Product1", 10.00m, 10);
        OrderProduct orderProduct2 = new(Guid.NewGuid(), "Product2", 10.00m, 10);

        Order order = new Order(Guid.NewGuid());
        order.AddProductOrder(orderProduct1);
        order.AddProductOrder(orderProduct2);

        //Act
        var total = order.CalcTotal;

        //Assert
        Assert.Equal(200m, total);
    }
}
