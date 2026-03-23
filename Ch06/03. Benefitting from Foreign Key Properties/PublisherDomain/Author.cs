namespace PublisherDomain;

public class Author
{
    public Author()
    {
        Books = new List<Book>();
    }

    public int AuthorId { get; set; }

    public string FirstName { get; set; } = String.Empty;

    public string LastName { get; set; } = String.Empty;

    public List<Book> Books { get; set; }
}