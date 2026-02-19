using System;
using AutoMapper;
using MediatR;
using OrderSystem.Application.DTOs.Address;
using OrderSystem.Domain.Entities;
using OrderSystem.Domain.Repository;
using OrderSystem.Domain.UnitOfWork;

namespace OrderSystem.Application.Addresses.Commands.CreateAddress;

public class CreateAddressHandler(IAddressRepository repository, IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<CreateAddressCommand, AddressDto>
{
    public async Task<AddressDto> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        Address address = mapper.Map<Address>(request);

        var response = await repository.AddAsync(address);
        await unitOfWork.CommitAsync();

        AddressDto addressDto = mapper.Map<AddressDto>(response);

        return addressDto;
    }
}
