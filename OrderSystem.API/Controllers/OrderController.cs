using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderSystem.Application.Authorization;
using OrderSystem.Application.DTOs.Order;
using OrderSystem.Application.Orders.Commands.CreateOrder;
using OrderSystem.Application.Orders.Queries.GetOrderById;
using OrderSystem.Application.Orders.Queries.ListOrders;
using OrderSystem.Domain.Entities;

namespace OrderSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IMediator mediator) : ControllerBase
    {

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand createOrderCommand)
        {
            var userClaim = createClaim();
            var authorizationResponse = OrderAuthorization.CreateOrder(userClaim, createOrderCommand);
            if (!authorizationResponse.Success)
            {
                return Unauthorized(authorizationResponse.Message);
            }

            CreateOrderResponseDto response = await mediator.Send(createOrderCommand);

            return CreatedAtRoute("GetOrderById", new { id = response.Id }, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await mediator.Send(new ListOrdersQuery());
            return Ok(response);
        }

        [Authorize]
        [HttpGet("{id:guid}", Name = "GetOrderById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            OrderDto? response = await mediator.Send(new GetOrderByIdQuery(id));
            if (response == null)
                return NotFound("Order Not Found");

            var userClaim = createClaim();
            var authorizationResponse = OrderAuthorization.GetById(userClaim, response);
            if (!authorizationResponse.Success)
            {
                return Unauthorized(authorizationResponse.Message);
            }

            return Ok(response);

        }

        UserClaim createClaim()
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = User?.FindFirst(ClaimTypes.Email)?.Value;
            var username = User?.FindFirst(ClaimTypes.Name)?.Value;
            var role = User?.FindFirst(ClaimTypes.Role)?.Value;

            if (userId == null)
                throw new NullReferenceException("Null UserId Claim");

            if (email == null)
                throw new NullReferenceException("Null email Claim");

            if (username == null)
                throw new NullReferenceException("Null username Claim");

            if (role == null)
                throw new NullReferenceException("Null role Claim");

            UserClaim userClaim = new UserClaim() { Id = userId, Email = email, Username = username, Role = role };

            return userClaim;
        }
    }
}
