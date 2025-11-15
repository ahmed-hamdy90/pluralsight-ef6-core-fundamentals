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
            new Author { AuthorId = 1, FirstName = "Rhoda", LastName = "Lerman" });

        // Prepare Array with Multi Author then use HasData method
        var authorList = new Author[] {
            new Author { AuthorId = 2, FirstName = "Ruth", LastName = "Ozeki" },
            new Author { AuthorId = 3, FirstName = "Sofia", LastName = "Segovia" },
            new Author { AuthorId = 4, FirstName = "Ursula K.", LastName = "LeGuin" },
            new Author { AuthorId = 5, FirstName = "Hugh", LastName = "Howey" },
            new Author { AuthorId = 6, FirstName = "Isabelle", LastName = "Allende" },
        };
        modelBuilder.Entity<Author>().HasData(authorList);

        // Prepare Array with Multi Books then use HasData method
        var someBooks = new Book[]
        {
            new Book { BookId = 1, AuthorId = 1, Title = "In God's Ear", PublishDate = new DateTime(1989, 3, 1) },
            new Book { BookId = 2, AuthorId = 2, Title = "A Tale For the Time Being", PublishDate = new DateTime(2013, 12, 31) },
            new Book { BookId = 3, AuthorId = 3, Title = "The Left Hand of Darkness", PublishDate = new DateTime(1969, 3, 1) }
        };

        modelBuilder.Entity<Book>().HasData(someBooks);
    }
}