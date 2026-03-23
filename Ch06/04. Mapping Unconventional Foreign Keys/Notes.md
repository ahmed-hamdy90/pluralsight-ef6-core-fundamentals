
### When Need to Make UnConventional FK Foreign Key for Author/Book Relation:


1. Edit `Book` Entity with new FK not related to (Author + Id) like previous

```csharp
namespace PublisherDomain;

public class Book
{
    public int BookId { get; set; }

    // ...

    public Author? Author { get; set; } = null;

    // public int AuthorId { get; set; } // Old Author + Id

    public int AuthorFK { get; set; }
}
```

2. Use Fluent API to Define the new UnConventional FK

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
            .HasForeignKey(book => book.AuthorFK);
    }
}
```

