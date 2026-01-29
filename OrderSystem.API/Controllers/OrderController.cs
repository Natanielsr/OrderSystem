using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderSystem.Application.DTOs;
using OrderSystem.Application.Orders.Commands.CreateOrder;

namespace OrderSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IMediator mediator) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderDto createOrderDto)
        {
            CreateOrderCommand command = new CreateOrderCommand(createOrderDto);
            var response = await mediator.Send(command);

            return CreatedAtRoute(nameof(Create), response, response.OrderId);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            return Ok();
        }
    }
}
