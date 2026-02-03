using System;
using AutoMapper;
using MediatR;
using OrderSystem.Application.DTOs.User;
using OrderSystem.Domain.Entities;
using OrderSystem.Domain.Repository;
using OrderSystem.Domain.Services;
using OrderSystem.Domain.UnitOfWork;

namespace OrderSystem.Application.Users.Commands.CreateUser;

public class CreateUserHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IPasswordService passwordService
    ) : IRequestHandler<CreateUserCommand, CreateUserResponseDto>
{
    public async Task<CreateUserResponseDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        User user = mapper.Map<User>(request);
        user.SetDefaultEntityProps();
        user.SetPasswordService(passwordService);
        user.HashPassword(request.Password);

        var response = await userRepository.AddAsync(user);
        await unitOfWork.CommitAsync();

        var createOrderResponseDto = mapper.Map<CreateUserResponseDto>(response);
        return createOrderResponseDto;
    }
}
