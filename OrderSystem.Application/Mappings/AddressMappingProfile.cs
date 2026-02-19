using System;
using AutoMapper;
using OrderSystem.Application.Addresses.Commands.CreateAddress;
using OrderSystem.Application.DTOs.Address;
using OrderSystem.Domain.Entities;

namespace OrderSystem.Application.Mappings;

public class AddressMappingProfile : Profile
{
    public AddressMappingProfile()
    {
        CreateMap<CreateAddressCommand, Address>();
        CreateMap<Address, AddressDto>();
    }
}
