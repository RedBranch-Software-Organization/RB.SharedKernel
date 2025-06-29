using Microsoft.Extensions.Configuration;

namespace RB.SharedKernel.MongoDb.Extensions;

public static class ConfigurationExtensions
{
    public static IMongoDatabase GetMongoDatabase(this IConfiguration configuration, string name)
    {
        var connectionString = configuration.GetConnectionString(name);
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentException($"Connection string {name} is not configured.");

        using var client = new MongoClient(connectionString);
        return client.GetDatabase(name);
    }
}
