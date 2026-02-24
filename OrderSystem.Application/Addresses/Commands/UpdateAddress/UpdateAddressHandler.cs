using System;
using AutoMapper;
using MediatR;
using OrderSystem.Application.DTOs.Address;
using OrderSystem.Domain.Entities;
using OrderSystem.Domain.Repository;
using OrderSystem.Domain.UnitOfWork;

namespace OrderSystem.Application.Addresses.Commands.UpdateAddress;

public class UpdateAddressHandler(IAddressRepository repository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateAddressCommand, AddressDto>
{
    public async Task<AddressDto> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        Address address = mapper.Map<Address>(request);
        Address response = (Address)await repository.UpdateAsync(request.Id, address);

        var success = await unitOfWork.CommitAsync();
        if (!success)
            throw new Exception("It was not possible to update the address in the repository.");

        AddressDto addressDto = mapper.Map<AddressDto>(response);

        return addressDto;

    }
}
