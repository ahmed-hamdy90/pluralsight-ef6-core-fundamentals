
### When Need to Adding NewBook without Author Details, We Three Options:


1. Edit `Book` Entity with (Author + Id) property To be Accept NULL

```csharp
namespace PublisherDomain;

public class Book
{
    public int BookId { get; set; }

    // ...

    public Author? Author { get; set; } = null;

    public int? AuthorId { get; set; }

}
```

2. Use Fluent API to Define the FK as Optional Value If we have already Foreign Key Property (Author + Id)

```csharp
namespace PublisherData;

using Microsoft.EntityFrameworkCore;
using PublisherDomain;

public class PubContext: DbContext
{
    // ...

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ...
        modelBuilder.Entity<Author>()
            .HasMany(author => author.Books)
            .WithOne(book => book.Author)
            .HasForeignKey(book => book.AuthorId)
            .IsRequired(false);
    }
}
```

3. Use Fluent API to Define the FK as Optional Value If we have not Foreign Key Property (Author + Id)

```csharp
namespace PublisherData;

using Microsoft.EntityFrameworkCore;
using PublisherDomain;

public class PubContext: DbContext
{
    // ...

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ...
        modelBuilder.Entity<Author>()
            .HasMany(author => author.Books)
            .WithOne(book => book.Author)
            .HasForeignKey("AuthorId")
            .IsRequired(false);
    }
}
```
