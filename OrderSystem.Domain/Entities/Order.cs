namespace OrderSystem.Domain.Entities;

public class Order : Entity
{
    public List<ProductOrder> ProductsOrder { get; private set; } = new List<ProductOrder>();
    public Guid UserId { get; set; }
    public User? OrderUser { get; set; }

    public string UserName { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;

    public decimal Total()
    {
        if (ProductsOrder is null)
            throw new Exception("ProductsOrder cant be null");

        decimal total = 0;
        foreach (ProductOrder p in ProductsOrder)
        {
            total += p.Total();
        }

        return total;
    }

    public void AddProductOrder(ProductOrder productOrder)
    {
        ProductsOrder.Add(productOrder);
    }
}
