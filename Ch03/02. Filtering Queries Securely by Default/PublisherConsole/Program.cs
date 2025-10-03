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


void QueryFilters()
{
    // First Way to Filter with direct value inside Where Method
    // var authors = _context.Authors.Where(author => author.FirstName == "Josie").ToList();
    // This Way will Generate Query like This: SELECT * FROM Authors WHERE Authors.FirstName == N'Josie';

    // Second Way for Dynamic Filter with Variable
    var name = "Josie";
    var authors = _context.Authors.Where(author => author.FirstName == name).ToList();
    // This Way will Generate Query like This: @P1 =N'Josie'; SELECT * FROM Authors WHERE Authors.FirstName == @P1;

    foreach (var author in authors)
    {
        Console.WriteLine(
            author.FirstName + " " + author.LastName
        );
    }
}

QueryFilters();
