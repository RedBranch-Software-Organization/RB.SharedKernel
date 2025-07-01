# RB.SharedKernel

Repository contains custom base classes that you can use as a base for your domain entities and value objects

[![RB.SharedKernel](https://github.com/RedBranch-Software-Organization/RB.SharedKernel/actions/workflows/build-rb-sharedkernel.yml/badge.svg?branch=master&event=workflow_dispatch)](https://github.com/RedBranch-Software-Organization/RB.SharedKernel/actions/workflows/build-rb-sharedkernel.yml)
[![RB.SharedKernel.MediatR](https://github.com/RedBranch-Software-Organization/RB.SharedKernel/actions/workflows/build-rb-sharedkernel-mediatr.yml/badge.svg?branch=master&event=workflow_dispatch)](https://github.com/RedBranch-Software-Organization/RB.SharedKernel/actions/workflows/build-rb-sharedkernel-mediatr.yml)
[![RB.SharedKernel.MongoDb](https://github.com/RedBranch-Software-Organization/RB.SharedKernel/actions/workflows/build-rb-sharedkernel-mongodb.yml/badge.svg?branch=master&event=workflow_dispatch)](https://github.com/RedBranch-Software-Organization/RB.SharedKernel/actions/workflows/build-rb-sharedkernel-mongodb.yml)

- [RB.SharedKernel](#rbsharedkernel)
  - [Install](#install)
  - [Version](#version)
  - [Usage](#usage)
    - [RB.SharedKernel (Core)](#rbsharedkernel-core)
      - [ValueObject](#valueobject)
      - [Entity and IAggregateRoot](#entity-and-iaggregateroot)
    - [RB.SharedKernel.Extensions](#rbsharedkernelextensions)
      - [DateTimeExtensions](#datetimeextensions)
        - [IsBetween](#isbetween)
    - [RB.SharedKernel.MediatR](#rbsharedkernel-mediatr)
      - [IResult](#iresult)
    - [RB.SharedKernel.MongoDb](#rbsharedkernel-mongodb)
      - [IDocument<T>](#idocumentt)
      - [IRepository<TEntity, TIdentifier>](#irepositorytentity-tidentifier)
      - [RepositoryBase<TEntity, TIdentifier>](#repositorybasetentity-tidentifier)
      - [Extensions](#extensions)
        - [ConfigurationExtensions.GetMongoDatabase](#configurationextensionsgetmongodatabase)
        - [MongoDatabaseExtensions.CreateCollectionIfNotExistsAsync](#mongodatabaseextensionscreatecollectionifnotexistsasync)

## Install

To use these libraries, add the desired NuGet packages to your project:

```sh
# For the core ValueObject, Entity, and base extensions
dotnet add package RB.SharedKernel --version 0.1.0

# For MediatR related utilities like IResult
dotnet add package RB.SharedKernel.MediatR --version 0.1.0

# For MongoDB abstractions like IRepository and RepositoryBase
dotnet add package RB.SharedKernel.MongoDb --version 0.1.0
```

## Version

| Package Name | Version Number | Target Framework |
|-----------------|-----------------|-----------------|
| RB.SharedKernel | [![0.1.0](https://img.shields.io/badge/0.1.0-gray?style=flat-square)](https://github.com/RedBranch-Software-Organization/RB.SharedKernel/pkgs/nuget/RB.SharedKernel) | ![.NET 9.0](https://img.shields.io/badge/.NET%209.0-blue?style=flat-square) |
| RB.SharedKernel.MediatR | [![0.1.0](https://img.shields.io/badge/0.1.0-gray?style=flat-square)](https://github.com/RedBranch-Software-Organization/RB.SharedKernel/pkgs/nuget/RB.SharedKernel.MediatR) | ![.NET 9.0](https://img.shields.io/badge/.NET%209.0-blue?style=flat-square) |
| RB.SharedKernel.MongoDb | [![0.1.0](https://img.shields.io/badge/0.1.0-gray?style=flat-square)](https://github.com/RedBranch-Software-Organization/RB.SharedKernel/pkgs/nuget/RB.SharedKernel.MongoDb) | ![.NET 9.0](https://img.shields.io/badge/.NET%209.0-blue?style=flat-square) |

## Usage

### RB.SharedKernel (Core)

#### ValueObject

```csharp
public class Address : ValueObject
{
    public String Street { get; private set; }
    public String City { get; private set; }
    public String State { get; private set; }
    public String Country { get; private set; }
    public String ZipCode { get; private set; }

    public Address() { }

    public Address(string street, string city, string state, string country, string zipcode)
    {
        Street = street;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipcode;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        // Using a yield return statement to return each element one at a time
        yield return Street;
        yield return City;
        yield return State;
        yield return Country;
        yield return ZipCode;
    }
}
```

#### Entity and IAggregateRoot

```csharp
public class Order : Entity<Guid>, IAggregateRoot
{
    private DateTime _orderDate;
    public Address Address { get; private set; }

    public Order(Guid id, Address address) : base(id)
    {
        _orderDate = DateTime.UtcNow;
        Address = address;
    }
}
```

### RB.SharedKernel.Extensions

#### DateTimeExtensions

##### IsBetween

```csharp
DateTime xMass = DateTime.Parse("2024-12-25 00:00:00")
DateTime newYearsEve = DateTime.Parse("2024-12-31 00:00:00");
DateTime dateToCheck = DateTime.Parse("2024-12-28 00:00:00");
bool isBetween = dateToCheck.IsBetween(xMass, newYearsEve);
```

### RB.SharedKernel.MediatR

#### IResult

The `IResult` interface is a marker interface that can be used to define the result of a MediatR request (query or command). This helps in standardizing the way results are handled in CQRS patterns.

```csharp
// Example of a command result implementing IResult
public class CreateOrderResult : IResult
{
    public bool Success { get; set; }
    public Guid OrderId { get; set; }
    public IEnumerable<string> Errors { get; set; }
}

// Example of a MediatR handler returning IResult
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, IResult>
{
    public async Task<IResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        // ... logic to create order ...

        if (orderSuccessfullyCreated)
        {
            return new CreateOrderResult { Success = true, OrderId = order.Id };
        }
        else
        {
            return new CreateOrderResult { Success = false, Errors = new[] { "Failed to create order." } };
        }
    }
}
```

### RB.SharedKernel.MongoDb

This library provides a set of tools and abstractions for working with MongoDB in .NET applications.

#### IDocument<T>

The `IDocument<T>` interface defines a contract for MongoDB documents, ensuring they have an `Id` property. `T` represents the type of the Id.

```csharp
using RB.SharedKernel.MongoDb.Interfaces;
using MongoDB.Bson; // Required for ObjectId

// Example of a document using Guid as Id
public class ProductDocument : IDocument<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

// Example of a document using ObjectId as Id
public class UserDocument : IDocument<ObjectId>
{
    public ObjectId Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
}
```

#### IRepository<TEntity, TIdentifier>

The `IRepository<TEntity, TIdentifier>` interface provides a generic contract for repository patterns, offering standard CRUD operations for entities that implement `IDocument<TIdentifier>`.

- `TEntity`: The type of the entity, which must be a class, implement `IDocument<TIdentifier>`, and have a parameterless constructor.
- `TIdentifier`: The type of the entity's identifier, which cannot be null.

Methods:
- `Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)`: Retrieves all entities.
- `Task<TEntity> GetByIdAsync(TIdentifier id, CancellationToken cancellationToken = default)`: Retrieves an entity by its Id.
- `Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)`: Creates a new entity.
- `Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)`: Updates an existing entity.
- `Task<bool> DeleteAsync(TIdentifier id, CancellationToken cancellationToken = default)`: Deletes an entity by its Id.

#### RepositoryBase<TEntity, TIdentifier>

`RepositoryBase<TEntity, TIdentifier>` is an abstract class that provides a base implementation for the `IRepository<TEntity, TIdentifier>` interface. To use it, you need to inherit from this class and provide the collection name.

```csharp
using RB.SharedKernel.MongoDb;
using RB.SharedKernel.MongoDb.Interfaces;
using MongoDB.Driver; // Required for IMongoDatabase

// Concrete entity implementing IDocument
public class Customer : IDocument<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public Customer() // Required for RepositoryBase
    {
        Id = Guid.NewGuid();
    }
}

// Concrete repository implementation
public class CustomerRepository : RepositoryBase<Customer, Guid>
{
    public override string CollectionName => "customers"; // Specify the MongoDB collection name

    public CustomerRepository(IMongoDatabase database) : base(database)
    {
    }

    // You can add custom repository methods here if needed
    public async Task<Customer> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Customer>.Filter.Eq(c => c.Email, email);
        return await _database.GetCollection<Customer>(CollectionName) // _database is accessible from RepositoryBase
            .Find(filter)
            .FirstOrDefaultAsync(cancellationToken);
    }
}

// Usage example:
// var client = new MongoClient("your_connection_string");
// var database = client.GetDatabase("your_database_name");
// var customerRepository = new CustomerRepository(database);
//
// var newCustomer = new Customer { Name = "John Doe", Email = "john.doe@example.com" };
// await customerRepository.CreateAsync(newCustomer);
//
// var retrievedCustomer = await customerRepository.GetByIdAsync(newCustomer.Id);
// Console.WriteLine($"Retrieved Customer: {retrievedCustomer.Name}");
```

#### Extensions

##### ConfigurationExtensions.GetMongoDatabase

This extension method for `IConfiguration` simplifies retrieving an `IMongoDatabase` instance using connection string names defined in your application's configuration.

```csharp
// Assuming you have IConfiguration service configured (e.g., in ASP.NET Core)
// And a connection string named "MongoDbConnection" in your appsettings.json:
// {
//   "ConnectionStrings": {
//     "MongoDbConnection": "mongodb://localhost:27017/mydatabase"
//   }
// }

using Microsoft.Extensions.Configuration; // For IConfiguration
using RB.SharedKernel.MongoDb.Extensions; // For GetMongoDatabase
using MongoDB.Driver; // For IMongoDatabase

// In your service configuration (e.g., Startup.cs or Program.cs)
// IConfiguration configuration = ...; // Get IConfiguration instance
// IMongoDatabase database = configuration.GetMongoDatabase("MongoDbConnection");
// services.AddSingleton(database); // Register IMongoDatabase for DI
```

##### MongoDatabaseExtensions.CreateCollectionIfNotExistsAsync

This extension method for `IMongoDatabase` allows you to create a MongoDB collection if it doesn't already exist.

```csharp
using RB.SharedKernel.MongoDb.Extensions; // For CreateCollectionIfNotExistsAsync
using MongoDB.Driver; // For IMongoDatabase

// IMongoDatabase database = ...; // Get IMongoDatabase instance
// string newCollectionName = "myNewCollection";
// await database.CreateCollectionIfNotExistsAsync(newCollectionName);
// Console.WriteLine($"Collection '{newCollectionName}' ensured to exist.");
```
