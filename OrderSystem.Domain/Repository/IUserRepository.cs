
using OrderSystem.Domain.Entities;

namespace OrderSystem.Domain.Repository;

public interface IUserRepository : IRepository
{
    public Task<User> GeByUserNameAsync(string username);
    public Task<User> GetByEmailAsync(string email);
}
