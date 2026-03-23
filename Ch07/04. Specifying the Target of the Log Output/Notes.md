

### When Need to Track Output EF Core Logs, We have Different Output Store Options As Delegates:

1. Console (Used within previous Solution)

```csharp
namespace PublisherData;

using Microsoft.EntityFrameworkCore;

public class PubContext: DbContext
{
    // ...

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var sqlConnectionString = "";

        optionsBuilder
            .UseSqlServer(sqlConnectionString)
            .LogTo(Console.WriteLine);
    }
}
```

2. File via FileStream

```csharp
namespace PublisherData;

using Microsoft.EntityFrameworkCore;

public class PubContext: DbContext
{
    // ...

    private StreamWriter _writer =
        new StreamWriter("EFCoreLog.txt", append: true);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var sqlConnectionString = "";

        optionsBuilder
            .UseSqlServer(sqlConnectionString)
            .LogTo(_writer.WriteLine);
    }

    // As We Have to Dispose the StreamWriter
    public override void Dispose()
    {
        _writer.Dispose();
        base.Dispose();
    }
}
```

3. Debug Window

```csharp
namespace PublisherData;

using Microsoft.EntityFrameworkCore;

public class PubContext: DbContext
{
    // ...

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var sqlConnectionString = "";

        optionsBuilder
            .UseSqlServer(sqlConnectionString)
            .LogTo(log => Debug.WriteLine(log));
    }
}
```

4. External Logger APIs - No Code Snipt as Differet APIs implemenation
