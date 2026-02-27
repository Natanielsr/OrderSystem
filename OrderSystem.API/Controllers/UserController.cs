using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderSystem.Application.DTOs.User;
using OrderSystem.Application.Users.Commands.Auth;
using OrderSystem.Application.Users.Commands.CreateUser;
using OrderSystem.Application.Users.Commands.GetUser;

namespace OrderSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand createUserCommand)
        {
            CreateUserResponseDto response = await mediator.Send(createUserCommand);
            return CreatedAtRoute("GetUserById", new { id = response.Id }, response);
        }


        [HttpGet("{id:guid}", Name = "GetUserById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await mediator.Send(new GetUserCommand(id));

            return Ok(response);
        }


    }
}
