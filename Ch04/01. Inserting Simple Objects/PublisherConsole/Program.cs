// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;
using System.Linq;

// Keep this Line just for Make sture Program Begin
Console.WriteLine("Console App Running");

// Make sure SQL Database be created and Ready to Use
using (PubContext context = new PubContext())
{
    context.Database.EnsureCreated();
}

// Now Declare Context outside Method
using PubContext _context = new PubContext();


void InsertAuthor()
{
    var author = new Author { FirstName = "Frank", LastName = "Herbert" };

    // Now we Can Add Author Without Depends on Authos DbSet
    // EF DbContext will check the give Entity and define which DbSet needed to be used
    // Also Depends on Entity's Status
    _context.Add(author);

    _context.SaveChanges();
}

void DisplayAuthors()
{
    // Note: We here use AsNoTracking Method to Remove useless EF DbContext Track Entities Status
    var authors = _context.Authors.AsNoTracking().ToList();

    authors.ForEach(author => Console.WriteLine($"{author.FirstName} {author.LastName}"));
}

Console.WriteLine("InsertAuthor Method Execute ====");
InsertAuthor();
Console.WriteLine("DisplayAuthors Method Execute ====");
DisplayAuthors();
