using System;
using OrderSystem.Domain.Entities;

namespace OrderSystem.Tests.Domain.Entities;

public class OrderProductTest
{
    [Fact]
    public void TestTotal()
    {
        //Arrange
        OrderProduct orderProduct = new(Guid.NewGuid(), 10m, 10);

        //Act
        var total = orderProduct.Total;

        //Assert
        Assert.Equal(100m, total);
    }
}
