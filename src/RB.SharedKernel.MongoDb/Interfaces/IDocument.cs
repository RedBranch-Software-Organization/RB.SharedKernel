using MongoDB.Bson.Serialization.Attributes;

namespace RB.SharedKernel.MongoDb.Interfaces;

public interface IDocument<T>
{
    [BsonId]
    [BsonElement("id")]
    public T Id { get; set; }
}
