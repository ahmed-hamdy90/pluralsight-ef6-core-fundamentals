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

    // Use this next Method For Build our Seeding with EF Migration
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Use one Line Seed using HasData method
        modelBuilder.Entity<Author>().HasData(
            new Author { Id = 1, FirstName = "Rhoda", LastName = "Lerman" });

        // Prepare Array with Multi Author then use HasData method
        var authorList = new Author[] {
            new Author { Id = 2, FirstName = "Ruth", LastName = "Ozeki" },
            new Author { Id = 3, FirstName = "Sofia", LastName = "Segovia" },
            new Author { Id = 4, FirstName = "Ursula K.", LastName = "LeGuin" },
            new Author { Id = 5, FirstName = "Hugh", LastName = "Howey" },
            new Author { Id = 6, FirstName = "Isabelle", LastName = "Allende" },
        };
        modelBuilder.Entity<Author>().HasData(authorList);
    }
}