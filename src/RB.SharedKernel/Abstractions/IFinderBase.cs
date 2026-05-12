using Ardalis.Specification;

namespace RB.SharedKernel.Abstractions;

public interface IFinderBase<T> : IReadRepositoryBase<T>
    where T : class { }
