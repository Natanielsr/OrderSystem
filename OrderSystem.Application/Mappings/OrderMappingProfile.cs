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
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTimeOffset.UtcNow))
            .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => DateTimeOffset.UtcNow))
            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => DateTimeOffset.UtcNow))
            .ForMember(dest => dest.OrderProducts, opt => opt.Ignore());

        CreateMap<Order, OrderDto>();
        CreateMap<OrderProduct, OrderProductDto>();

        CreateMap<PaymentInfo, PaymentInfoDto>();
    }
}
