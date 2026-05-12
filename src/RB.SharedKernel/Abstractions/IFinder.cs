namespace RB.SharedKernel.Abstractions;

public interface IFinder<T> : IFinderBase<T> 
    where T : class, IAggregateRoot { }
