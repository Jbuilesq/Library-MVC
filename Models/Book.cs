namespace LibraryMVC.Models;

public class Book
{
    public int Id { get; set; }
    public string CodeId { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int NumberOfShares { get; set; }
    public ICollection<Loan> loans { get; set; } = new List<Loan>();

    public Book(string codeId, string title, string author, int numberOfShares)
    {
        CodeId = codeId;
        Title = title;
        Author = author;
        NumberOfShares = numberOfShares;
    }
    public Book(){}
}