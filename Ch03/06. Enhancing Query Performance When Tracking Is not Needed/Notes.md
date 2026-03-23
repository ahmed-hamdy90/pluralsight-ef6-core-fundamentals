### For Control on Disable Model Tracking

1. Setting NoTracking configuration under Data Set (Db Set) within Query

```csharp
var query =
    _context.Authors.AsNoTracking().FirstOrDefault();
```

2. Use In General Configuration under Define SQL Server connection within Context

```csharp
public class PubContext: DbContext
{
    public DbSet<Author> Authors { get; set; }

    public DbSet<Book> Books { get; set; }

    // Use Hard Code Configuration
    // TODO: Replaced, AS now used for learning purpose 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(
                @"Server=tcp:mssql,1433;Database=PubDatabase;User Id=SA;Password=P@ass_LocalSql19;TrustServerCertificate=true;"
            )
            // Set the global default behavior to NoTracking
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
}
```
***Note:*** in case Make one Db Set with Tracking option again, Use the next way
```csharp
var query =
    _context.Authors.AsTracking().FirstOrDefault();
```
