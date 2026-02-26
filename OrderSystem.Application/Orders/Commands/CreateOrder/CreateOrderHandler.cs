using AutoMapper;
using MediatR;
using OrderSystem.Application.DTOs.Order;
using OrderSystem.Application.Services;
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
        var user = await GetUser(request.UserId);
        if (user == null)
            throw new UserNotFoundException();

        var orderId = Guid.NewGuid();

        var orderProducts = await createValidOrderProductListAndReduceInStock(request.OrderProducts, orderId);

        Order order = new()
        {
            Id = orderId,
            CreationDate = DateTimeOffset.UtcNow,
            UpdateDate = DateTimeOffset.UtcNow,
            Active = true,
            UserName = user.Username,
            UserEmail = user.Email,
            Total = Order.CalcTotal(orderProducts),
            Status = OrderStatus.Pending,
            Code = GenerateCode.Generate(),
            UserId = user.Id,
            AddressId = request.AddressId,
            OrderProducts = orderProducts
        };

        order.PaymentInfo.Add(new PaymentInfo()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTimeOffset.UtcNow,
            UpdateDate = DateTimeOffset.UtcNow,
            Active = true,
            Method = request.PaymentMethod,
            PaidAmount = order.Total,
            TransactionReference = "transaction_reference",
            LastFourDigits = "last_four_digits",
            ProviderName = "provider_name",
            Status = PaymentStatus.Pending,
            OrderId = order.Id
        });


        Order createdOrder = (Order)await orderUnitOfWork.orderRepository.AddAsync(order);
        var success = await orderUnitOfWork.CommitAsync();

        if (!success)
            throw new Exception("Error processing the order");

        CreateOrderResponseDto createOrderResponseDto = mapper.Map<CreateOrderResponseDto>(createdOrder);

        return createOrderResponseDto;
    }

    private async Task<User> GetUser(Guid userId)
    {
        return (User)await userRepository.GetByIdAsync(userId);
    }

    private async Task<List<OrderProduct>> createValidOrderProductListAndReduceInStock(
        List<CreateOrderProductDto> createOrderProductDtos,
        Guid orderId
        )
    {
        List<OrderProduct> orderProducts = new List<OrderProduct>();

        if (HasDuplicates(createOrderProductDtos))
            throw new DuplicateProductInOrderException();

        foreach (var productDto in createOrderProductDtos)
        {
            Product product = (Product)await orderUnitOfWork.productRepository.GetByIdAsync(productDto.ProductId);
            if (product is null)
                throw new ProductNotFoundException();


            OrderProduct orderProduct = new()
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTimeOffset.UtcNow,
                UpdateDate = DateTimeOffset.UtcNow,
                Active = true,
                ProductId = product.Id,
                ProductName = product.Name,
                UnitPrice = product.Price,
                Quantity = productDto.Quantity,
                OrderId = orderId
            };

            orderProducts.Add(orderProduct);

            product.ReduceInStock(productDto.Quantity); //reduce product in stock
            await orderUnitOfWork.productRepository.UpdateAsync(product.Id, product); //update in repository
        }

        return orderProducts;
    }

    private bool HasDuplicates(List<CreateOrderProductDto> list)
    {
        // Compara o total de itens com o total de IDs únicos
        return !(list.Select(x => x.ProductId).Distinct().Count() == list.Count);
    }
}
