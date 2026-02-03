using AutoMapper;
using MediatR;
using OrderSystem.Application.DTOs;
using OrderSystem.Domain.Entities;
using OrderSystem.Domain.Exceptions;
using OrderSystem.Domain.Repository;
using OrderSystem.Domain.UnitOfWork;

namespace OrderSystem.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler(
    IOrderUnitOfWork orderUnitOfWork,
    IMapper mapper,
    IUserRepository userRepository
    )
: IRequestHandler<CreateOrderCommand, CreateOrderResponseDto>
{
    public async Task<CreateOrderResponseDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        Order order = mapper.Map<Order>(request);

        var isValid = await isValidUser(order.UserId);
        if (!isValid)
            throw new UserNotFoundException();

        order = await addProducts(request.OrderProducts, order);

        Order createdOrder = (Order)await orderUnitOfWork.orderRepository.AddAsync(order);
        var success = await orderUnitOfWork.CommitAsync();

        if (!success)
            throw new Exception("Error processing the order");

        CreateOrderResponseDto createOrderResponseDto = mapper.Map<CreateOrderResponseDto>(createdOrder);

        return createOrderResponseDto;
    }

    private async Task<bool> isValidUser(Guid userId)
    {
        User user = (User)await userRepository.GetByIdAsync(userId);
        if (user is null)
            return false;
        else
            return true;
    }

    private async Task<Order> addProducts(List<CreateOrderProductDto> createOrderProductDtos, Order order)
    {
        if (HasDuplicates(createOrderProductDtos))
            throw new DuplicateProductInOrderException();

        foreach (var productDto in createOrderProductDtos)
        {
            Product product = (Product)await orderUnitOfWork.productRepository.GetByIdAsync(productDto.ProductId);
            if (product is null)
                throw new ProductNotFoundException();


            OrderProduct orderProduct = new(
                product.Id,
                product.Price,
                productDto.Quantity
            );

            orderProduct.SetDefaultEntityProps();

            order.AddProductOrder(orderProduct);

            product.ReduceInStock(productDto.Quantity);
            await orderUnitOfWork.productRepository.UpdateAsync(product.Id, product);

        }

        return order;
    }

    private bool HasDuplicates(List<CreateOrderProductDto> list)
    {
        // Compara o total de itens com o total de IDs únicos
        return !(list.Select(x => x.ProductId).Distinct().Count() == list.Count);
    }
}
