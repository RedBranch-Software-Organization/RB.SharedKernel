namespace RB.SharedKernel.Abstractions;

public interface IDomainEvent
{
    Guid Id { get; }
    DateTime OccurredAt { get; }
}