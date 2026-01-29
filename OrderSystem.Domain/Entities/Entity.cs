namespace OrderSystem.Domain.Entities;

public abstract class Entity
{
    public Guid Id { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public DateTimeOffset UpdateDate { get; set; }

    public bool Active { get; set; }


}
