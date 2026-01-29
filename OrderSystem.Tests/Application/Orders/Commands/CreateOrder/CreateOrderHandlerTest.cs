using AutoMapper;
using Moq;
using OrderSystem.Application.DTOs;
using OrderSystem.Application.Mappings;
using OrderSystem.Application.Orders.Commands.CreateOrder;
using OrderSystem.Domain.Entities;
using OrderSystem.Domain.Exceptions;
using OrderSystem.Domain.UnitOfWork;

namespace OrderSystem.Tests.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandlerTest
{
    private readonly CreateOrderHandler createOrderHandler;
    private readonly IMapper _mapper;

    private Mock<IOrderUnitOfWork> mockOrderUnitOfWork;
    CancellationToken cancellationToken = new CancellationToken();

    private readonly List<Product> TestProducts = new List<Product>()
    {
        new () { Id = Guid.NewGuid(), AvailableQuantity = 1},
        new () { Id = Guid.NewGuid(), AvailableQuantity = 2},
        new () { Id = Guid.NewGuid(), AvailableQuantity = 3},
    };

    private readonly Guid OrderId = Guid.NewGuid();

    public CreateOrderHandlerTest()
    {

        mockOrderUnitOfWork = new Mock<IOrderUnitOfWork>();
        // 1. Configura o AutoMapper com seus Profiles reais
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<OrderMappingProfile>(); // Substitua pelo seu Profile real
        });
        _mapper = config.CreateMapper();

        foreach (var product in TestProducts)
        {
            mockOrderUnitOfWork.Setup(m => m.productRepository.GetById(product.Id)).ReturnsAsync(product);
        }
        mockOrderUnitOfWork.Setup(m => m.orderRepository.Add(It.IsAny<Order>())).ReturnsAsync(OrderId);
        mockOrderUnitOfWork.Setup(m => m.CommitAsync()).ReturnsAsync(true);

        createOrderHandler = new CreateOrderHandler(mockOrderUnitOfWork.Object, _mapper);
    }

    [Fact]
    public async Task CreateOrderHandlerSuccessTest()
    {
        //Arrange

        List<ProductOrderDto> productOrderDtos = new List<ProductOrderDto>();

        foreach (var product in TestProducts)
        {
            productOrderDtos.Add(new() { ProductId = product.Id, Quantity = product.AvailableQuantity });
        }

        CreateOrderDto createOrderDto = new() { UserId = Guid.NewGuid(), Products = productOrderDtos };
        CreateOrderCommand command = new(createOrderDto);

        //Act
        var response = await createOrderHandler.Handle(command, cancellationToken);

        //Assert
        Assert.Equal(OrderId, response.OrderId);
        // BÔNUS: Verificar se o método foi chamado exatamente uma vez
        mockOrderUnitOfWork.Verify(m => m.productRepository.GetById(It.IsAny<Guid>()), Times.Exactly(3));
        mockOrderUnitOfWork.Verify(m => m.orderRepository.Add(It.IsAny<Order>()), Times.Once);
    }

    [Fact]
    public async Task CreateOrderHandlerWithProductNotFoundErrorTest()
    {
        //Arrange

        List<ProductOrderDto> productOrderDtos = new List<ProductOrderDto>()
        {
            new() { ProductId = Guid.NewGuid(), Quantity = 1 }
        };

        CreateOrderDto createOrderDto = new() { UserId = Guid.NewGuid(), Products = productOrderDtos };
        CreateOrderCommand command = new(createOrderDto);

        //Act
        var er = await Assert.ThrowsAsync<ProductNotFoundException>(async () =>
        {
            var response = await createOrderHandler.Handle(command, cancellationToken);
        });

        Assert.Equal("Product Id in order doesn't exist", er.Message);

        // BÔNUS: Verificar se o método foi chamado exatamente uma vez
        mockOrderUnitOfWork.Verify(m => m.productRepository.GetById(It.IsAny<Guid>()), Times.Once);
        mockOrderUnitOfWork.Verify(m => m.orderRepository.Add(It.IsAny<Order>()), Times.Never);
    }

}
