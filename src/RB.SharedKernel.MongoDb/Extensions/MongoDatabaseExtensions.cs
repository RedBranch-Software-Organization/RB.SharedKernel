using MongoDB.Driver;
using MongoDB.Bson;
namespace RB.SharedKernel.MongoDb.Extensions;

public static class MongoDatabaseExtensions
{
    private const string Message = "Collection name cannot be null or empty.";

    public static async Task CreateCollectionIfNotExistsAsync(this IMongoDatabase database, string collectionName)
    {
        ArgumentNullException.ThrowIfNull(database);
        if (string.IsNullOrWhiteSpace(collectionName))
            throw new ArgumentException(Message, nameof(collectionName));

        var collections = await database.ListCollectionsAsync(new ListCollectionsOptions { Filter = new BsonDocument("name", collectionName) });

        if (!await collections.AnyAsync())
            await database.CreateCollectionAsync(collectionName);
    }
}
