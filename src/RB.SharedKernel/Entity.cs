namespace RB.SharedKernel;

public abstract class Entity 
{
    public int Id { get; set; }
}

public abstract class Entity<TId>
  where TId : struct, IEquatable<TId>
{
    public TId Id { get; set; } = default!;
}

public abstract class Entity<T, TId>
  where T : Entity<T, TId>
{
    public TId Id { get; set; } = default!;
}