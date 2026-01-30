namespace OrderSystem.Application.DTOs;

public record class ProductOrderResponseDto
{
    public required Guid ProductId { get; set; }
    public required int Quantity { get; set; }
    public required decimal UnitPrice { get; set; }
}
