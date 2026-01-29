namespace OrderSystem.Application.DTOs;

public record class CreateOrderDto
{
    public required List<ProductOrderDto> Products { get; set; }

    public required Guid UserId { get; set; }
}
