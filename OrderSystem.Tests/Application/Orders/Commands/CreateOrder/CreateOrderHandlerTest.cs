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
        new () { Id = Guid.NewGuid(), AvailableQuantity = 1, Price = 1},
        new () { Id = Guid.NewGuid(), AvailableQuantity = 2, Price = 2},
        new () { Id = Guid.NewGuid(), AvailableQuantity = 3, Price = 3},
    };
    Order? order;
    private readonly Guid OrderId = Guid.NewGuid();

    public CreateOrderHandlerTest()
    {
        _mapper = mockAutoMapper();
        mockUnitOfWork();
        createMockOrder();

        createOrderHandler = new CreateOrderHandler(mockOrderUnitOfWork!.Object, _mapper);
    }

    IMapper mockAutoMapper()
    {
        // 1. Configura o AutoMapper com seus Profiles reais
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<OrderMappingProfile>(); // Substitua pelo seu Profile real
        });
        return config.CreateMapper();
    }

    void mockUnitOfWork()
    {
        mockOrderUnitOfWork = new Mock<IOrderUnitOfWork>();
        foreach (var product in TestProducts)
        {
            mockOrderUnitOfWork.Setup(m => m.productRepository.GetById(product.Id)).ReturnsAsync(product);
        }

        mockOrderUnitOfWork.Setup(m => m.CommitAsync()).ReturnsAsync(true);
    }

    void createMockOrder()
    {
        order = new();
        order.Id = OrderId;

        foreach (var product in TestProducts)
        {
            order.AddProductOrder(new()
            {
                ProductId = product.Id,
                Quantity = product.AvailableQuantity,
                UnitPrice = product.Price
            });
        }
        mockOrderUnitOfWork.Setup(m => m.orderRepository.Add(It.IsAny<Order>())).ReturnsAsync(order);
    }

    CreateOrderCommand createOrderCommand()
    {
        List<ProductOrderDto> productOrderDtos = new List<ProductOrderDto>();
        foreach (var product in TestProducts)
        {
            productOrderDtos.Add(new() { ProductId = product.Id, Quantity = product.AvailableQuantity });
        }
        CreateOrderCommand command = new(productOrderDtos, Guid.NewGuid());

        return command;
    }

    [Fact]
    public async Task CreateOrderHandlerSuccessTest()
    {
        //Arrange
        CreateOrderCommand command = createOrderCommand();

        //Act
        var response = await createOrderHandler.Handle(command, cancellationToken);

        //Assert
        Assert.Equal(OrderId, response.OrderId);

        // BÔNUS: Verificar se o método foi chamado exatamente uma vez
        mockOrderUnitOfWork.Verify(m => m.orderRepository.Add(It.IsAny<Order>()), Times.Once);
    }

    [Fact]
    public async Task CreateOrderProductIdTest()
    {
        //Arrange
        CreateOrderCommand command = createOrderCommand();

        //Act
        var response = await createOrderHandler.Handle(command, cancellationToken);

        //Assert
        Assert.Equal(3, response.ProductOrderResponseDtos.Count);
        for (int i = 0; i < TestProducts.Count; i++)
        {
            Product testProduct = TestProducts.ElementAt(i);
            ProductOrderResponseDto responseProduct = response.ProductOrderResponseDtos.ElementAt(i);

            Assert.Equal(testProduct.Id, responseProduct.ProductId);
        }

        // BÔNUS: Verificar se o método foi chamado exatamente uma vez
        mockOrderUnitOfWork.Verify(m => m.productRepository.GetById(It.IsAny<Guid>()), Times.Exactly(3));
    }

    [Fact]
    public async Task CreateOrderProductPriceTest()
    {
        //Arrange
        CreateOrderCommand command = createOrderCommand();

        //Act
        CreateOrderResponseDto response = await createOrderHandler.Handle(command, cancellationToken);

        //Assert
        Assert.Equal(3, response.ProductOrderResponseDtos.Count);
        for (int i = 0; i < TestProducts.Count; i++)
        {
            Product testProduct = TestProducts.ElementAt(i);
            ProductOrderResponseDto responseProduct = response.ProductOrderResponseDtos.ElementAt(i);

            Assert.Equal(testProduct.Price, responseProduct.UnitPrice);
        }

        // BÔNUS: Verificar se o método foi chamado exatamente uma vez
        mockOrderUnitOfWork.Verify(m => m.productRepository.GetById(It.IsAny<Guid>()), Times.Exactly(3));
    }

    [Fact]
    public async Task CreateOrderProductQuantityTest()
    {
        //Arrange
        CreateOrderCommand command = createOrderCommand();

        //Act
        var response = await createOrderHandler.Handle(command, cancellationToken);

        //Assert
        Assert.Equal(3, response.ProductOrderResponseDtos.Count);
        for (int i = 0; i < TestProducts.Count; i++)
        {
            ProductOrderDto productOrderDtoRequest = command.Products.ElementAt(i);
            ProductOrderResponseDto responseProduct = response.ProductOrderResponseDtos.ElementAt(i);

            Assert.Equal(productOrderDtoRequest.Quantity, responseProduct.Quantity);
        }

        // BÔNUS: Verificar se o método foi chamado exatamente uma vez
        mockOrderUnitOfWork.Verify(m => m.productRepository.GetById(It.IsAny<Guid>()), Times.Exactly(3));
    }

    [Fact]
    public async Task CreateOrderHandlerWithProductNotFoundErrorTest()
    {
        //Arrange

        List<ProductOrderDto> productOrderDtos = new List<ProductOrderDto>()
        {
            new() { ProductId = Guid.NewGuid(), Quantity = 1 }
        };

        CreateOrderCommand command = new(productOrderDtos, Guid.NewGuid());

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
