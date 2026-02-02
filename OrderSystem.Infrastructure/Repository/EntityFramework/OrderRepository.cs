using System;
using Microsoft.EntityFrameworkCore;
using OrderSystem.Domain.Entities;
using OrderSystem.Domain.Repository;
using OrderSystem.Infrastructure.Data;

namespace OrderSystem.Infrastructure.Repository.EntityFramework;

public class OrderRepository(AppDbContext context) : IOrderRepository
{
    public async Task<Entity> AddAsync(Entity entity)
    {
        await context.Orders.AddAsync((Order)entity);

        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var order = await context.Orders.FindAsync(id);

        if (order != null)
        {
            context.Orders.Remove(order);
            return true;
        }

        return false;
    }

    public async Task<IEnumerable<Entity>> GetAllAsync()
    {
        return await context.Orders.AsNoTracking().ToListAsync();
    }

    public async Task<Entity> GetByIdAsync(Guid id)
    {
        var order = await context.Orders.FindAsync(id);
        return order!;
    }

    public async Task<Entity> UpdateAsync(Guid id, Entity updatedEntity)
    {
        var order = await context.Orders.FindAsync(id);

        if (order != null)
        {
            order = (Order)updatedEntity;
        }

        return order!;
    }
}
