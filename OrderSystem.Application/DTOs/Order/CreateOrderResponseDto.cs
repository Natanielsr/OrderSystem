namespace OrderSystem.Application.DTOs.Order;

public record class CreateOrderResponseDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public required List<CreateOrderProductResponseDto> OrderProducts { get; set; }

    public decimal Total { get; set; }
}
