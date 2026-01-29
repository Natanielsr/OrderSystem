using System;
using AutoMapper;
using OrderSystem.Application.DTOs;
using OrderSystem.Domain.Entities;

namespace OrderSystem.Application.Mappings;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<Order, CreateOrderResponseDto>();

        CreateMap<CreateOrderDto, Order>()
            .ForMember(
                dest => dest.ProductsOrder,
                opt => opt.Ignore()
            ); ;
    }
}
