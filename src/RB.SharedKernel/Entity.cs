namespace RB.SharedKernel;
public abstract class Entity<TId>(TId id)
{
    public TId Id { get; set; } = id;
}
