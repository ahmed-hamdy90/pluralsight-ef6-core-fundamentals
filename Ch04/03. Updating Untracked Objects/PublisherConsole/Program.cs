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

void DisplayAllAuthors()
{
    // Note: We here use AsNoTracking Method to Remove useless EF DbContext Track Entities Status
    var authors = _context.Authors.AsNoTracking().ToList();

    authors.ForEach(author => Console.WriteLine($"{author.FirstName} {author.LastName}"));
}

// Next Method will depends on Separated DbContext
// To simulate Update without need to Track Entity's Status
Author FindThatAuthor(int authorId)
{
    using var shortLivedContext = new PubContext();

    return shortLivedContext.Authors.Find(authorId); // Ignore NULL Reference now
}

void SaveThatAuthor(Author author)
{
    using var anotherShortLivedContext = new PubContext();

    anotherShortLivedContext.Authors.Update(author);
    anotherShortLivedContext.SaveChanges();
}

void CoordinatedRetrieveAndUpdateAuthor()
{
    var author = FindThatAuthor(3);
    if (author?.FirstName == "Julie")
    {
        author.FirstName = "Julia";
        SaveThatAuthor(author);    
    }
}

Console.WriteLine("CoordinatedRetrieveAndUpdateAuthor Method Execute =======");
CoordinatedRetrieveAndUpdateAuthor();

Console.WriteLine("DisplayAllAuthors Method Execute =======");
DisplayAllAuthors();
