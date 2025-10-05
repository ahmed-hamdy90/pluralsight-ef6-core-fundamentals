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

// Console.WriteLine("SortAuthors Method Execute =======");
// SortAuthors();

void QueryAggregate()
{
    // First Use LINQ Method to Get First Author
    var filter1 = "Lerman";
    var firstAuthor =
        _context.Authors.Where(author => author.LastName == filter1).FirstOrDefault();
    // OR
    //    _context.Authors.FirstOrDefault(author => author.LastName == filter1); // Direct not need to use Where

    Console.WriteLine($"{firstAuthor.FirstName} {firstAuthor.LastName}");

    // Second Use LINQ Methos to Get First Author After Sorting them
    var firstAuthorDescending =
        _context.Authors.OrderByDescending(author => author.FirstName)
                        .FirstOrDefault(author => author.LastName == filter1);

    // Third Check Invalid Filter and how will Return NULL result
    var filter2 = "Lurman";
    var invalidAuthor = _context.Authors.FirstOrDefault(author => author.LastName == filter2);

    if (invalidAuthor == null)
    {
        Console.WriteLine($"No Author with LastName Equal: {filter2}");
    }

    // Finally Check use LINQ LastOrDefault method without Sorting
    try
    {
        // This Line will Throw InvalidOperation Exception
        var lastAuthor = _context.Authors.LastOrDefault(author => author.LastName == filter1);
    }
    catch (Exception e)
    {
        Console.WriteLine($"Exception {e.GetType()}, Message: {e.Message}");
    }
}

// Console.WriteLine("QueryAggregate Method Execute =======");
// QueryAggregate();

void RetrieveAndUpdateAuthor()
{
    // First Exists User
    var author =
        _context.Authors.FirstOrDefault(author => author.FirstName == "Julie" && author.LastName == "Lerman");
    if (author != null)
    {
        // Update its First name
        author.FirstName = "Julia";
        // Then Run Chnages based on Entity Status
        _context.SaveChanges();
    }
}

void RetrieveAndUpdateMultipleAuthors()
{
    // getting all Authors with Last name as Lerman
    var LermanAuthors =
        _context.Authors.Where(author => author.LastName == "Lerman").ToList();

    foreach (var lermanAuthor in LermanAuthors)
    {
        lermanAuthor.LastName = "Lehrman";
    }

    // Now For Learn How EF works to determine Changes for Entities
    Console.WriteLine("Before Call DetectChanges");
    Console.WriteLine("Status: " + _context.ChangeTracker.DebugView.ShortView);

    _context.ChangeTracker.DetectChanges(); // This Method Automatic Called within SaveChanges method

    Console.WriteLine("After Call DetectChanges");
    Console.WriteLine("Status: " + _context.ChangeTracker.DebugView.ShortView);

    _context.SaveChanges();
}

void VeriousOperations()
{
    // Now we will make Two Actions once: Update exists Author and Add new Author
    var author = _context.Authors.Find(2); // Second Author with Joise
    author.FirstName = "Newfoundland";

    var newAuthor = new Author { FirstName = "Appleman", LastName = "Dan" };
    _context.Authors.Add(newAuthor);

    _context.SaveChanges();

}

Console.WriteLine("RetrieveAndUpdateAuthor Method Execute =======");
RetrieveAndUpdateAuthor();

Console.WriteLine("RetrieveAndUpdateMultipleAuthors Method Execute =======");
RetrieveAndUpdateMultipleAuthors();

Console.WriteLine("VeriousOperations Method Execute =======");
VeriousOperations();

Console.WriteLine("DisplayAllAuthors Method Execute =======");
DisplayAllAuthors();
