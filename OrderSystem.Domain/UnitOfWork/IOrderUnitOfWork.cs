using OrderSystem.Domain.Repository;

namespace OrderSystem.Domain.UnitOfWork;

public interface IOrderUnitOfWork : IDisposable
{
    IOrderRepository orderRepository { get; }
    IProductRepository productRepository { get; }

    Task<bool> CommitAsync();
}
