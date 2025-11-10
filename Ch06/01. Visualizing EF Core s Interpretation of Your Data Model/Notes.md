
### For Control on Table's field name or configuration we have three ways:

1. Use Default Property declaration in Entity Class: `property name = column name`

```csharp
namespace PublisherDomain;

public class Book
{
    public string Title { get; set; } = String.Empty;
}
```

2. Use Fluent API to override the configuration outside Entity Class

```csharp
namespace PublisherData;

using Microsoft.EntityFrameworkCore;
using PublisherDomain;

public class PubContext: DbContext
{
    // ...

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().Property(book => book.Title).HasColumnName("MainTitle");
        // ...
    }
}
```

3. Use Data Annotation to override the configuration inside Entity Class

***NOTE:*** First Must install [Microsoft.EntityFrameworkCore.Abstractions](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Abstractions/) package To able use Data Annotation

```csharp
namespace PublisherDomain;
namespace System.ComponentModel.DataAnnotations.Schema;

public class Book
{
    [Column("MainTitle")]
    public string Title { get; set; } = String.Empty;
}
```
