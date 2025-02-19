# RB.SharedKernel
Repository contains custom base classes that you can use as a base for your domain entities and value objects
[![Build, Test, Pack & Push Nuget Package Manual](https://github.com/RedBranch-Software-Organization/RB.SharedKernel/actions/workflows/build.yml/badge.svg)](https://github.com/RedBranch-Software-Organization/RB.SharedKernel/actions/workflows/build.yml)
## Install
```sh
dotnet add package RB.SharedKernel --version 0.1.0-beta.1.0
```
## Version
| Version Number | Target Framework | 
|-----------------|-----------------|
| [![0.1.0-beta.1.0](https://img.shields.io/badge/0.1.0--beta.1.0-gray?style=flat-square)](https://github.com/RedBranch-Software-Organization/RB.SharedKernel/pkgs/nuget/RB.SharedKernel/356462575) | ![.NET 9.0](https://img.shields.io/badge/.NET%209.0-blue?style=flat-square) |
## Usage
### ValueObject
```csharp
public class EmailAddress : ValueObject
{
    public string Value { get; }

    public EmailAddress(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email cannot be empty.", nameof(value));

        if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ArgumentException("Invalid email format.", nameof(value));

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value.ToLower();
    }

    public override string ToString() => Value;
}
```
### Entity
```csharp
public class User : Entity<Guid>
{
    public EmailAddress Email { get; private set; }

    public User(Guid id, EmailAddress email) : base(id)
    {
        Email = email ?? throw new ArgumentNullException(nameof(email));
    }

    public void ChangeEmail(EmailAddress newEmail)
    {
        Email = newEmail ?? throw new ArgumentNullException(nameof(newEmail));
    }
}
```