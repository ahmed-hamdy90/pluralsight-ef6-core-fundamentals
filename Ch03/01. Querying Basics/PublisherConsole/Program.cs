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

void GetAuthors()
{
    using var context = new PubContext();

    // Getting Result Through DbSet call Database Once (Most Efficient Way)
    var authors = context.Authors.ToList();
    foreach (var author in authors)
    {
        Console.WriteLine(
            author.FirstName + " " + author.LastName
        );
    }

    // Another Way to Getting Result with use Query as Enumurator But Risky as Connection still open Until Complete Loop
    // foreach (var author in context.Authors)
    // {
    //     Console.WriteLine(
    //         author.FirstName + " " + author.LastName
    //     );
    // }

    // Third Way to use LINQ Query to Getting Result
    // var authors = (from a in context.Authors select a).ToList();

    // foreach (var author in authors)
    // {
    //     Console.WriteLine(
    //         author.FirstName + " " + author.LastName
    //     );
    // }
}

GetAuthors();
