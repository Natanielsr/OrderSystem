using System;
using AutoMapper;
using OrderSystem.Application.DTOs;
using OrderSystem.Application.Orders.Commands.CreateOrder;
using OrderSystem.Domain.Entities;

namespace OrderSystem.Application.Mappings;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<Order, CreateOrderResponseDto>();

        CreateMap<CreateOrderCommand, Order>()
            .ForMember(
                dest => dest.ProductsOrder,
                opt => opt.Ignore()
            );
    }
}
