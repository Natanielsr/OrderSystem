using System;
using Microsoft.EntityFrameworkCore;
using OrderSystem.Domain.Entities;
using OrderSystem.Domain.Repository;
using OrderSystem.Infrastructure.Data;

namespace OrderSystem.Infrastructure.Repository.EntityFramework;

public class AddressRepository(AppDbContext context) : IAddressRepository
{
    public async Task<Entity> AddAsync(Entity entity)
    {
        Address address = (Address)entity;
        address.SetDefaultEntityProps();

        await context.Addresses.AddAsync((Address)entity);

        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var address = await context.Addresses.FindAsync(id);

        if (address != null)
        {
            context.Addresses.Remove(address);
            return true;
        }

        return false;
    }

    public async Task<IEnumerable<Entity>> GetAllAsync()
    {
        return await context.Addresses
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Entity> GetByIdAsync(Guid id)
    {
        var address = await context.Addresses
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == id);

        return address!;
    }

    public async Task<List<Address>> GetUserAddressesAsync(Guid UserId, int page, int pageSize)
    {
        return await context.Addresses
            .AsNoTracking()
            .Where(a => a.UserId == UserId)
            .OrderByDescending(o => o.CreationDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Entity> UpdateAsync(Guid id, Entity updatedEntity)
    {
        var address = await context.Addresses.FindAsync(id);

        if (address != null)
        {
            address.RenewUpdateDate();
            address = (Address)updatedEntity;
        }

        return address!;
    }
}
