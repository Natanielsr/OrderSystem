using AutoMapper;
using OrderSystem.Application.DTOs.Order;
using OrderSystem.Application.Orders.Commands.CreateOrder;
using OrderSystem.Domain.Entities;

namespace OrderSystem.Application.Mappings;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<OrderProduct, CreateOrderProductResponseDto>();
        CreateMap<Order, CreateOrderResponseDto>();

        CreateMap<CreateOrderCommand, Order>()
            .ForMember(dest => dest.OrderProducts, opt => opt.Ignore());

        CreateMap<Order, OrderDto>();
        CreateMap<OrderProduct, OrderProductDto>();
    }
}
