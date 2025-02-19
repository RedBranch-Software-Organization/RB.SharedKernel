# RB.SharedKernel
Repository contains custom base classes that you can use as a base for your domain entities and value objects

[![Build, Test, Pack & Push Nuget Package Manual](https://github.com/RedBranch-Software-Organization/RB.SharedKernel/actions/workflows/build.yml/badge.svg)](https://github.com/RedBranch-Software-Organization/RB.SharedKernel/actions/workflows/build.yml)
- [RB.SharedKernel](#rbsharedkernel)
  - [Install](#install)
  - [Version](#version)
  - [Usage](#usage)
    - [RB.SharedKernel](#rbsharedkernel-1)
      - [ValueObject](#valueobject)
      - [Entity and IAggregateRoot](#entity-and-iaggregateroot)
    - [RB.SharedKernel.Extensions](#rbsharedkernelextensions)
      - [DateTimeExtensions](#datetimeextensions)
      - [IsBetween](#isbetween)
## Install
```sh
dotnet add package RB.SharedKernel --version 0.1.0-beta.1.0
```
## Version
| Version Number | Target Framework | 
|-----------------|-----------------|
| [![0.1.0-beta.1.0](https://img.shields.io/badge/0.1.0--beta.1.0-gray?style=flat-square)](https://github.com/RedBranch-Software-Organization/RB.SharedKernel/pkgs/nuget/RB.SharedKernel/356462575) | ![.NET 9.0](https://img.shields.io/badge/.NET%209.0-blue?style=flat-square) |
## Usage
### RB.SharedKernel
#### ValueObject
<details>
<summary>Show code</summary>
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
</details>
#### Entity and IAggregateRoot
<details>
<summary>Show code</summary>
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
</details>
### RB.SharedKernel.Extensions
#### DateTimeExtensions
#### IsBetween
<details>
<summary>Show code</summary>
```csharp
DateTime xMass = DateTime.Parse("2024-12-25 00:00:00")
DateTime newYearsEve = DateTime.Parse("2024-12-31 00:00:00");
DateTime dateToCheck = DateTime.Parse("2024-12-28 00:00:00");
bool isBetween = dateToCkech.IsBetween(xMass, newYearsEve);
```
</summary>
