namespace PublisherData;

using Microsoft.EntityFrameworkCore;
using PublisherDomain;

public class PubContext: DbContext
{
    public DbSet<Author> Authors { get; set; }

    public DbSet<Book> Books { get; set; }
}