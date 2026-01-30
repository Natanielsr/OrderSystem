using System;
using OrderSystem.Domain.Entities;
using OrderSystem.Domain.Repository;

namespace OrderSystem.Infrastructure.Repository;

public class ProductRepositoryTEST : IProductRepository
{
    List<Product> products = new List<Product>()
    {
        new (){ Id= Guid.NewGuid(), Name = "Monitor", Price = 1, AvailableQuantity = 1},
        new (){ Id= Guid.NewGuid(), Name = "Teclado", Price = 2, AvailableQuantity = 2},
        new (){ Id= Guid.NewGuid(), Name = "Mouse", Price = 3, AvailableQuantity = 3},
    };

    public async Task<Entity> Add(Entity entity)
    {
        products.Add((Product)entity);
        return entity;
    }

    public Task<bool> Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Entity>> GetAll()
    {
        return products;
    }

    public async Task<Entity> GetById(Guid Id)
    {
        return products.FirstOrDefault(p => p.Id == Id)!;
    }
}
