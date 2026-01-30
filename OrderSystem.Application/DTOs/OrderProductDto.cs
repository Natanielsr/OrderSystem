namespace OrderSystem.Application.DTOs;

public record class OrderProductDto
{
    public Guid ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }

}
