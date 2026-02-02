using System;
using OrderSystem.Domain.Entities;
using OrderSystem.Domain.Repository;

namespace OrderSystem.Infrastructure.Repository;

public class UserRepositoryTEST : IUserRepository
{
    List<User> users = new List<User>()
    {
        new (Guid.NewGuid()){ Name = "TestUser1", Email = "testuser1@email.com" },
        new (Guid.NewGuid()){ Name = "TestUser2", Email = "testuser2@email.com" },
        new (Guid.NewGuid()){ Name = "TestUser3", Email = "testuser3@email.com" },
    };

    public UserRepositoryTEST()
    {
        users.ForEach(u => Console.WriteLine($"{u.Name} : {u.Id}"));
    }

    public async Task<Entity> AddAsync(Entity entity)
    {
        users.Add((User)entity);

        return entity;
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Entity>> GetAllAsync()
    {
        return users;
    }

    public async Task<Entity> GetByIdAsync(Guid id)
    {
        return users.FirstOrDefault(u => u.Id == id)!;
    }

    public Task<Entity> UpdateAsync(Guid id, Entity updatedEntity)
    {
        throw new NotImplementedException();
    }
}
