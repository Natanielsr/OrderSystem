namespace OrderSystem.Application.DTOs.Order;

public record class OrderDto
{
    public Guid Id { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public DateTimeOffset UpdateDate { get; set; }
    public List<OrderProductDto>? OrderProducts { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
    public bool Active { get; set; }
    public decimal Total { get; set; }

}
