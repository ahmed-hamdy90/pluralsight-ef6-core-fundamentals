namespace PublisherDomain;

public class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = String.Empty;

    public DateTime PublishDate { get; set; }

    public decimal BasePrice { get; set; }

    public Author? Author { get; set; } = null;

    public int AuthorId { get; set; }

    // public int AuthorFK { get; set; } // Example of Mapping UnConvestional FK

    // public int? AuthorId { get; set; } // Option #1: To Enable of Adding Book without Author
}