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
        decimal total = 0;
        foreach (ProductOrder p in ProductsOrder)
        {
            total += p.Total();
        }

        return total;
    }

    public void AddProductOrder(ProductOrder productOrder)
    {
        if (productOrder is null)
            throw new Exception("productOrder cant be null");

        if (productOrder.Quantity <= 0)
            throw new Exception("productOrder quantity must be bigger then zero");

        if (productOrder.UnitPrice <= 0)
            throw new Exception("productOrder UnitPrice must be bigger then zero");


        ProductsOrder.Add(productOrder);
    }
}
