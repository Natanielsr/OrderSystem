namespace OrderSystem.Domain.Entities;

public class Order : Entity
{
    public List<OrderProduct> OrderProducts { get; private set; } = new List<OrderProduct>();
    public Guid UserId { get; set; }
    public User? OrderUser { get; set; }

    public string UserName { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;

    public decimal Total()
    {
        decimal total = 0;
        foreach (OrderProduct p in OrderProducts)
        {
            total += p.Total();
        }

        return total;
    }

    public void AddProductOrder(OrderProduct productOrder)
    {
        if (productOrder is null)
            throw new Exception("productOrder cant be null");

        if (productOrder.Quantity <= 0)
            throw new Exception("productOrder quantity must be bigger then zero");

        if (productOrder.UnitPrice <= 0)
            throw new Exception("productOrder UnitPrice must be bigger then zero");


        OrderProducts.Add(productOrder);
    }
}
