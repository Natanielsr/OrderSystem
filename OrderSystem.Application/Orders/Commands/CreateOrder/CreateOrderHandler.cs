using AutoMapper;
using MediatR;
using OrderSystem.Application.DTOs;
using OrderSystem.Domain.Entities;
using OrderSystem.Domain.Exceptions;
using OrderSystem.Domain.UnitOfWork;

namespace OrderSystem.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler(
    IOrderUnitOfWork orderUnitOfWork,
    IMapper mapper
    )
: IRequestHandler<CreateOrderCommand, CreateOrderResponseDto>
{
    public async Task<CreateOrderResponseDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        CreateOrderDto createOrderDto = request.createOrderDto;

        Order order = mapper.Map<Order>(createOrderDto);

        foreach (var productDto in createOrderDto.Products)
        {
            Product product = (Product)await orderUnitOfWork.productRepository.GetById(productDto.ProductId);
            if (product is null)
                throw new ProductNotFoundException();

            ProductOrder productOrder = new ProductOrder()
            {
                ProductId = product.Id,
                Quantity = productDto.Quantity,
            };

            order.AddProductOrder(productOrder);
            product.ReduceInStock(productDto.Quantity);

        }

        Guid OrderId = await orderUnitOfWork.orderRepository.Add(order);
        var success = await orderUnitOfWork.CommitAsync();

        if (!success)
            throw new Exception("Error processing the order");

        return new CreateOrderResponseDto() { OrderId = OrderId };
    }
}
