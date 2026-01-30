namespace OrderSystem.Application.DTOs;

public record class CreateOrderProductDto
{
    public required Guid ProductId { get; set; }
    public required int Quantity { get; set; }

}
