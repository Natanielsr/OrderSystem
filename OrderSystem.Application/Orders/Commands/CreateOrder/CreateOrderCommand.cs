using MediatR;
using OrderSystem.Application.DTOs;

namespace OrderSystem.Application.Orders.Commands.CreateOrder;

public record class CreateOrderCommand(List<ProductOrderDto> Products, Guid UserId) : IRequest<CreateOrderResponseDto>
{

}
