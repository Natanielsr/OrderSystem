namespace OrderSystem.Application.DTOs;

public record class ProductOrderDto
{
    public required Guid ProductId { get; set; }
    public required int Quantity { get; set; }
}
