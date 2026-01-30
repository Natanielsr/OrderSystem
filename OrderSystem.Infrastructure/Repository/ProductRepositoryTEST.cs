using System;
using OrderSystem.Domain.Entities;
using OrderSystem.Domain.Repository;

namespace OrderSystem.Infrastructure.Repository;

public class ProductRepositoryTEST : IProductRepository
{
    public async Task<Entity> Add(Entity entity)
    {
        return entity;
    }

    public Task<bool> Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Entity>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Entity> GetById(Guid Id)
    {
        throw new NotImplementedException();
    }
}
