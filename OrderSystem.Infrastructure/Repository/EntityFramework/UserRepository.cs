using Microsoft.EntityFrameworkCore;
using OrderSystem.Domain.Entities;
using OrderSystem.Domain.Repository;
using OrderSystem.Infrastructure.Data;

namespace OrderSystem.Infrastructure.Repository.EntityFramework;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task<Entity> AddAsync(Entity entity)
    {
        await context.Users.AddAsync((User)entity);

        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var user = await context.Users.FindAsync(id);

        if (user != null)
        {
            context.Users.Remove(user);
            return true;
        }

        return false;
    }

    public async Task<IEnumerable<Entity>> GetAllAsync()
    {
        return await context.Users.AsNoTracking().ToListAsync();
    }

    public async Task<Entity> GetByIdAsync(Guid id)
    {
        var order = await context.Users.FindAsync(id);
        return order!;
    }

    public async Task<Entity> UpdateAsync(Guid id, Entity updatedEntity)
    {
        var user = await context.Users.FindAsync(id);

        if (user != null)
        {
            user = (User)updatedEntity;
        }

        return user!;
    }
}
