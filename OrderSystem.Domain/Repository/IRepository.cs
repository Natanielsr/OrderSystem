using OrderSystem.Domain.Entities;

namespace OrderSystem.Domain.Repository;

public interface IRepository
{
    public Task<Guid> Add(Entity entity);
    public Task<IEnumerable<Entity>> GetAll();
    public Task<Entity> GetById(Guid Id);
    public Task<bool> Delete(Guid id);
}
