namespace PublisherData;

using Microsoft.EntityFrameworkCore;
using PublisherDomain;

public class PubContext: DbContext
{
    public DbSet<Author> Authors { get; set; }

    public DbSet<Book> Books { get; set; }

    // Use Hard Code Configuration
    // TODO: Replaced
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Course Code
        // optionsBuilder.UseSqlServer(
        //     @"Data Source=(localdb)/MSSQLLocalDB;Initial Catalog=PubDatabase"
        // );

        // But I use SQL Server Docker image
        optionsBuilder.UseSqlServer(
            @"Server=sqldb;Database=PubDatabase;User Id=SA;Password=myPassword;"
        );
    }
}