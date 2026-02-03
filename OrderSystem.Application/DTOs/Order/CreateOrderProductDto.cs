namespace OrderSystem.Application.DTOs.Order;

public record class CreateOrderProductDto
{
    public required Guid ProductId { get; set; }
    public required int Quantity { get; set; }

}
