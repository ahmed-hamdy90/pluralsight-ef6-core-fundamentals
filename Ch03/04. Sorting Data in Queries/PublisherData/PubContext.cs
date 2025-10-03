namespace PublisherData;

using Microsoft.EntityFrameworkCore;
using PublisherDomain;

public class PubContext: DbContext
{
    public DbSet<Author> Authors { get; set; }

    public DbSet<Book> Books { get; set; }

    // Use Hard Code Configuration
    // TODO: Replaced, AS now used for learning purpose 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            @"Server=tcp:mssql,1433;Database=PubDatabase;User Id=SA;Password=P@ass_LocalSql19;TrustServerCertificate=true;"
        );
    }
}