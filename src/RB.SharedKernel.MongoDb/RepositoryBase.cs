using RB.SharedKernel.MongoDb.Interfaces;

namespace RB.SharedKernel.MongoDb
{
    public abstract class RepositoryBase<TEntity, TIdentifier>(IMongoDatabase database) : IRepository<TEntity, TIdentifier>
        where TEntity : class, IDocument<TIdentifier>, new()
        where TIdentifier : notnull
    {
        public abstract string CollectionName { get; }
        public readonly IMongoDatabase Database = database;

        public async Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var filter = Builders<TEntity>.Filter.Empty;
            return await Database.GetCollection<TEntity>(CollectionName)
                .Find(filter)
                .ToListAsync(cancellationToken);
        }

        public Task<TEntity> GetByIdAsync(TIdentifier id, CancellationToken cancellationToken = default)
        {
            if (EqualityComparer<TIdentifier>.Default.Equals(id, default))
                throw new ArgumentException("Identifier cannot be the default value.", nameof(id));

            var filter = Builders<TEntity>.Filter.Eq("_id", id);
            return Database.GetCollection<TEntity>(CollectionName)
                .Find(filter)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");

            return Database.GetCollection<TEntity>(CollectionName)
                .InsertOneAsync(entity, cancellationToken: cancellationToken)
                .ContinueWith(_ => entity, cancellationToken);
        }

        public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");

            var filter = Builders<TEntity>.Filter.Eq("_id", entity.Id);
            return Database.GetCollection<TEntity>(CollectionName)
                .FindOneAndReplaceAsync(filter, entity, cancellationToken: cancellationToken);
        }

        public Task<bool> DeleteAsync(TIdentifier id, CancellationToken cancellationToken = default)
        {
            if (EqualityComparer<TIdentifier>.Default.Equals(id, default))
                throw new ArgumentException("Identifier cannot be the default value.", nameof(id));

            var filter = Builders<TEntity>.Filter.Eq("_id", id);
            return Database.GetCollection<TEntity>(CollectionName)
                .DeleteOneAsync(filter, cancellationToken: cancellationToken)
                .ContinueWith(task => task.Result.DeletedCount > 0, cancellationToken);
        }
    }
}