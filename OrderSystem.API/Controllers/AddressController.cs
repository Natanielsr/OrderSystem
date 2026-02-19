using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderSystem.API.Security;
using OrderSystem.Application.Addresses.Commands.CreateAddress;
using OrderSystem.Application.Addresses.Queries.GetAddressById;
using OrderSystem.Application.Addresses.Queries.GetUserAddresses;
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

        [Authorize]
        [HttpGet("{id:guid}", Name = "GetAddressById")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            AddressDto? response = await mediator.Send(new GetAddressByIdQuery(id));
            if (response == null)
                return NotFound("Address Not Found");

            var userClaim = APIClaim.createUserClaim(User);
            var authResponse = AuthorizationBase.ValidUser(userClaim, response.UserId);
            if (!authResponse.Success)
            {
                return StatusCode(403, authResponse.Message);
            }

            return Ok(response);
        }

        [Authorize]
        [HttpGet("GetUserAddresses")]
        public async Task<IActionResult> GetUserAddresses([FromQuery] Guid userId, [FromQuery] int page = 1, [FromQuery] int pageSize = 5)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 5 : pageSize;

            var userClaim = APIClaim.createUserClaim(User);
            var authorizationResponse = AuthorizationBase.ValidUser(userClaim, userId);
            if (!authorizationResponse.Success)
                return StatusCode(403, authorizationResponse.Message);

            var response = await mediator.Send(new GetUserAddressesQuery(userId, page, pageSize));
            return Ok(response);
        }
    }
}
