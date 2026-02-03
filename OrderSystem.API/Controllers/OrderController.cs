using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderSystem.Application.DTOs.Order;
using OrderSystem.Application.Orders.Commands.CreateOrder;
using OrderSystem.Application.Orders.Queries.GetOrderById;
using OrderSystem.Application.Orders.Queries.ListOrders;

namespace OrderSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IMediator mediator) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand createOrderCommand)
        {
            CreateOrderResponseDto response = await mediator.Send(createOrderCommand);

            return CreatedAtRoute("GetOrderById", new { id = response.Id }, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await mediator.Send(new ListOrdersQuery());
            return Ok(response);
        }

        [HttpGet("{id:guid}", Name = "GetOrderById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await mediator.Send(new GetOrderByIdQuery(id));

            return Ok(response);

        }
    }
}
