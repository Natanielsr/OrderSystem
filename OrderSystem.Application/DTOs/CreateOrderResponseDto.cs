namespace OrderSystem.Application.DTOs;

public record class CreateOrderResponseDto
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public required List<ProductOrderResponseDto> ProductOrderResponseDtos;
}
