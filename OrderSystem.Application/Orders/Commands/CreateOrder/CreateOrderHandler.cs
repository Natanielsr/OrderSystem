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
        Order order = mapper.Map<Order>(request);
        order.SetNewEntity();

        order = await addProducts(request.Products, order);

        Order createdOrder = (Order)await orderUnitOfWork.orderRepository.Add(order);
        var success = await orderUnitOfWork.CommitAsync();

        if (!success)
            throw new Exception("Error processing the order");

        return new CreateOrderResponseDto()
        {
            UserId = order.Id,
            OrderId = createdOrder.Id,
            ProductOrderResponseDtos = createdOrder.ProductsOrder.Select(
                p => new ProductOrderResponseDto()
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity,
                    UnitPrice = p.UnitPrice
                }
            ).ToList()
        };
    }

    private async Task<Order> addProducts(List<ProductOrderDto> productOrderDtos, Order order)
    {
        foreach (var productDto in productOrderDtos)
        {
            Product product = (Product)await orderUnitOfWork.productRepository.GetById(productDto.ProductId);
            if (product is null)
                throw new ProductNotFoundException();

            ProductOrder productOrder = new ProductOrder()
            {
                ProductId = product.Id,
                Quantity = productDto.Quantity,
                UnitPrice = product.Price
            };

            order.AddProductOrder(productOrder);
            product.ReduceInStock(productDto.Quantity);

        }

        return order;
    }
}
