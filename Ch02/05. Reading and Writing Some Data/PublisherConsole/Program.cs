// See https://aka.ms/new-console-template for more information
using PublisherData;
using PublisherDomain;

Console.WriteLine("Hello, World!");

// Make sure SQL Database be created and Ready to Use
using (PubContext context = new PubContext())
{
    context.Database.EnsureCreated();
}

void GetAuthors()
{
    using var context = new PubContext();
    var authors = context.Authors.ToList();
    foreach (var author in authors)
    {
        Console.WriteLine(
            author.FirstName + " " + author.LastName
        );
    }
}

void AddAuthor()
{
    // First Author based on video Course
    // var author = new Author() { FirstName = "Julia", LastName= "Lerman" };

    // Second Author based on video Course
    var author = new Author() { FirstName = "Josie", LastName= "Newf" };

    using var context = new PubContext();
    context.Authors.Add(author);
    context.SaveChanges();
}

GetAuthors();
AddAuthor();
GetAuthors();
