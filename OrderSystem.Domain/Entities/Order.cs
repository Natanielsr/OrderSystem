namespace OrderSystem.Domain.Entities;

public class Order : Entity
{
    public List<OrderProduct> OrderProducts { get; private set; } = new List<OrderProduct>();
    public Guid UserId { get; private set; }
    public User? OrderUser { get; private set; }

    public string UserName { get; private set; } = string.Empty;
    public string UserEmail { get; private set; } = string.Empty;

    protected Order() { }

    public Order(Guid id) : base(id)
    {
        SetDefaultEntityProps();
    }

    public Order(Guid id,
        DateTimeOffset creationDate,
        DateTimeOffset updateDate,
        bool active,
        List<OrderProduct> orderProducts,
        Guid userId,
        User orderUser,
        string userName,
        string userEmail

        ) : base(id, creationDate, updateDate, active)
    {
        this.UserId = userId;
        this.OrderUser = orderUser;
        this.UserName = userName;
        this.UserEmail = userEmail;
    }

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
