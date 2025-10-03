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
    // Fitler with LIKE for Name

    // First Way: Use Native EF.Function
    // var authors = _context.Authors.Where(author => EF.Functions.Like(author.LastName, "L%")).ToList();

    // Second Way: The same First Way But Depends on Variable for Like value
    // var filter = "L%";
    // var authors = _context.Authors.Where(author => EF.Functions.Like(author.LastName, filter)).ToList();

    // Third way: Use LINQ Contains method
    var authors =
        _context.Authors.Where(author => author.LastName.Contains("L")).ToList();
    // OR 
        // _context.Authors.Where(author => author.LastName.StartsWith("L")).ToList();

    foreach (var author in authors)
    {
        Console.WriteLine(
            author.FirstName + " " + author.LastName
        );
    }
}

// Console.WriteLine("Query Filters Method Execute =======");
// QueryFilters();

void FindIt()
{
    var author = _context.Authors.Find(2);

    Console.WriteLine($"{author.FirstName} {author.LastName}");
}

// Console.WriteLine("FindIt Method Execute =======");
// FindIt();

void AddSomeMoreAuthor()
{
    _context.Authors.Add(new Author { FirstName = "Rhoda", LastName = "Lerman" });
    _context.Authors.Add(new Author { FirstName = "Don", LastName = "Jones" });
    _context.Authors.Add(new Author { FirstName = "Jim", LastName = "Christoper" });
    _context.Authors.Add(new Author { FirstName = "Stephen", LastName = "Haunts" });
    _context.SaveChanges();
}

void SkipAndTakeAuthor()
{
    // Create Method to Simulate Paging and Use Skip, Take LINQ Methods
    var groupSize = 2;

    for (int group = 0; group < 5; group++)
    {
        Console.WriteLine($"Group {group}:");

        var authors = _context.Authors.Skip(groupSize * group).Take(groupSize).ToList();
        foreach (var author in authors)
        {
            Console.WriteLine($"{author.FirstName} {author.LastName}");
        }
    }
}

// Console.WriteLine("AddSomeMoreAuthor And SkipAndTakeAuthor Methods Execute =======");
// AddSomeMoreAuthor();
// SkipAndTakeAuthor();

void SortAuthors()
{
    // This Next Way Wrong as EF will Ignore first Filter by Last name and only sort by the last one (First Name)
    // var authorsByLastName =
    //     _context.Authors
    //             .OrderBy(author => author.LastName)
    //             .OrderBy(author => author.FirstName)
    //             .ToList();

    // authorsByLastName.ForEach(author => Console.WriteLine($"{author.FirstName} {author.LastName}"));

    // Fix above using ThenBy LINQ Method
    var authorsByLastName =
        _context.Authors
                .OrderBy(author => author.LastName)
                .ThenBy(author => author.FirstName)
                .ToList();

    authorsByLastName.ForEach(author => Console.WriteLine($"{author.FirstName} {author.LastName}"));

    // Now apply Sorting Descending with LINQ methods which Supported Descending Order
    var authorsDescending =
        _context.Authors
                .OrderByDescending(author => author.LastName)
                .ThenByDescending(author => author.FirstName)
                .ToList();

    Console.WriteLine("**Descending Last and First**");
    authorsDescending.ForEach(author => Console.WriteLine($"{author.FirstName} {author.LastName}"));
}

SortAuthors();
