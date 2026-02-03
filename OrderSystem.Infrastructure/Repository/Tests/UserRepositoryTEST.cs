using System;
using OrderSystem.Domain.Entities;
using OrderSystem.Domain.Repository;

namespace OrderSystem.Infrastructure.Repository.Tests;

public class UserRepositoryTEST : IUserRepository
{
    List<User> users = new List<User>()
    {
        new (Guid.NewGuid(), "TestUser1","testuser1@email.com", "password" ),
        new (Guid.NewGuid(), "TestUser2","testuser2@email.com", "password" ),
        new (Guid.NewGuid(), "TestUser3","testuser3@email.com", "password" ),
    };

    public UserRepositoryTEST()
    {
        users.ForEach(u => Console.WriteLine($"{u.Username} : {u.Id}"));
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

    public Task<User> GeByUserNameAsync(string username)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Entity>> GetAllAsync()
    {
        return users;
    }

    public Task<User> GetByEmailAsync(string email)
    {
        throw new NotImplementedException();
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
