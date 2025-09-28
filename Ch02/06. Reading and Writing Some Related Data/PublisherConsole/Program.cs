// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
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

// GetAuthors();
// AddAuthor();
// GetAuthors();

void AddAuthorWithBook()
{
    // Define Author
    var author = new Author() { FirstName = "Julia", LastName = "Lerman" };
    // Define Books
    var book1 =
        new Book()
        {
            Title = "Programming Entity Framework",
            PublishDate = new DateTime(2009, 1, 1)
        };

    var book2 =
        new Book()
        {
            Title = "Programming Entity Framework 2nd Ed",
            PublishDate = new DateTime(2010, 8, 1)
        };
    // Now adding Books for defined Author
    author.Books.Add(book1);
    author.Books.Add(book2);

    // Adding To DB
    using var context = new PubContext();
    context.Authors.Add(author);
    context.SaveChanges();
}

void GetAuthorsWithBooks()
{
    using var context = new PubContext();
    // Load Stored Authors and their Books
    var authors =
        context.Authors.Include(author => author.Books).ToList();

    foreach (var author in authors)
    {
        Console.WriteLine(
            author.FirstName + " " + author.LastName
        );

        foreach (var book in author.Books)
        {
            Console.WriteLine("*" + book.Title);
        }
    }
}

GetAuthorsWithBooks();
AddAuthorWithBook();
GetAuthorsWithBooks();

