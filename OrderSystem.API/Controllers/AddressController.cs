using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderSystem.API.Security;
using OrderSystem.Application.Addresses.Commands.CreateAddress;
using OrderSystem.Application.Authorization;
using OrderSystem.Application.DTOs.Address;

namespace OrderSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateAddressCommand createAddressCommand)
        {
            var userClaim = APIClaim.createUserClaim(User);
            var authResponse = AuthorizationBase.ValidUser(userClaim, createAddressCommand.UserId);
            if (!authResponse.Success)
            {
                return StatusCode(403, authResponse.Message);
            }

            AddressDto response = await mediator.Send(createAddressCommand);

            return CreatedAtRoute("GetAddressById", new { id = response.Id }, response);
        }
    }
}
