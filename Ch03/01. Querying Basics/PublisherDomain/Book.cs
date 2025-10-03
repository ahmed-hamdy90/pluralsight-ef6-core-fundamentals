namespace PublisherDomain;

public class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = String.Empty;

    public DateTime PublishDate { get; set; }

    public decimal BasePrice { get; set; }

    public Author? Author { get; set; } = null;

    public int AuthorId { get; set; }
}