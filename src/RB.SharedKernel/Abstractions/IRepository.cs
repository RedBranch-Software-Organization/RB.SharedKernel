using Ardalis.Specification;

namespace RB.SharedKernel.Abstractions;

public interface IRepository<T> : IRepositoryBase<T> 
    where T : class, IAggregateRoot { }
