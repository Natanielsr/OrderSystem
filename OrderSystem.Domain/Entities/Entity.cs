namespace OrderSystem.Domain.Entities;

public abstract class Entity
{
    public Guid Id { get; private set; }
    public DateTimeOffset CreationDate { get; private set; }
    public DateTimeOffset UpdateDate { get; private set; }
    public bool Active { get; private set; }

    protected Entity() { }

    public Entity(Guid id)
    {
        this.Id = id;
    }

    public Entity(Guid id, DateTimeOffset creationDate, DateTimeOffset updateDate, bool active)
    {
        this.Id = id;
        this.CreationDate = creationDate;
        this.UpdateDate = updateDate;
        this.Active = active;
    }

    public void SetDefaultEntityProps()
    {
        CreationDate = DateTimeOffset.UtcNow;
        UpdateDate = DateTimeOffset.UtcNow;
        Active = true;
    }

    public void RenewUpdateDate()
    {
        UpdateDate = DateTimeOffset.UtcNow;
    }
}
