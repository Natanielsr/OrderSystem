namespace OrderSystem.Application.DTOs;

public record class CreateOrderResponseDto
{
    public Guid OrderId { get; set; }
}
