namespace RB.SharedKernel.MongoDb.Interfaces
{
    interface IRepository<TEntity, TIdentifier>
        where TEntity : class, IDocument<TIdentifier>, new()
        where TIdentifier : notnull
    {
        Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TEntity> GetByIdAsync(TIdentifier id, CancellationToken cancellationToken = default);
        Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(TIdentifier id, CancellationToken cancellationToken = default);
    }
}