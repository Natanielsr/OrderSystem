using System.Dynamic;
using OrderSystem.Domain.Exceptions;

namespace OrderSystem.Domain.Entities;

public enum OrderStatus
{
    Pending,
    Paid,
    Shipped,
    Canceled
}

public class Order : Entity
{
    public List<OrderProduct> OrderProducts { get; private set; } = new List<OrderProduct>();
    public Guid UserId { get; private set; }
    public User? User { get; set; }
    public string UserName { get; private set; } = string.Empty;
    public string UserEmail { get; private set; } = string.Empty;
    public decimal Total { get; set; }
    public OrderStatus Status { get; set; }
    public List<PaymentInfo> PaymentInfo { get; set; } = new List<PaymentInfo>();
    public string? Code { get; private set; }
    public Guid AddressId { get; set; }
    public Address? Address { get; set; }

    public decimal CalcTotal
    {
        get
        {
            decimal total = 0;
            foreach (OrderProduct p in OrderProducts)
            {
                total += p.Total;
            }

            return total;
        }
    }


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
        User user,
        string userName,
        string userEmail,
        string code

        ) : base(id, creationDate, updateDate, active)
    {
        this.UserId = userId;
        this.User = user;
        this.UserName = userName;
        this.UserEmail = userEmail;
        this.OrderProducts = orderProducts;
        this.Code = code;
    }

    public void AddProductOrder(OrderProduct productOrder)
    {
        if (productOrder is null)
            throw new AddProductOrderException("productOrder cant be null");

        if (productOrder.Quantity <= 0)
            throw new AddProductOrderException("productOrder quantity must be bigger then zero");

        if (productOrder.UnitPrice <= 0)
            throw new AddProductOrderException("productOrder UnitPrice must be bigger then zero");

        if (ProductExistsInOrder(productOrder.ProductId))
            throw new AddProductOrderException("ProductId already exists in productOrder");

        productOrder.SetDefaultEntityProps();
        OrderProducts.Add(productOrder);
    }

    private bool ProductExistsInOrder(Guid productId)
    {
        // "Existe algum produto onde o ID seja igual ao productId?"
        return OrderProducts.Any(x => x.ProductId == productId);
    }

    public void SetUsername(string? username)
    {
        if (username == null)
            throw new NullReferenceException("username is null");

        UserName = username;
    }

    public void SetEmail(string? email)
    {
        if (email == null)
            throw new NullReferenceException("email is null");

        UserEmail = email;
    }

    public string GenerateCode(int length = 8)
    {
        // Definimos os caracteres permitidos (Letras e Números)
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        Random random = new Random();

        // Geramos a string escolhendo caracteres aleatórios da lista acima
        char[] result = Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)])
            .ToArray();

        var resultString = new string(result);
        this.Code = resultString;

        return resultString;
    }

}
