using System.ComponentModel.DataAnnotations;

namespace LibraryMVC.Models;

public class Loan
{
    public int Id { get; set; }
    public DateTime DateLoan { get; set; }
    public DateTime DateReturn { get; set; }
    [Required]
    public int UserId { get; set; }
    [Required]
    public int BookId { get; set; }
    public Client Client { get; set; }
    public Book Book { get; set; }


    public Loan(DateTime dateLoan, DateTime dateReturn, int userId, int bookId)
    {
        DateLoan = dateLoan;
        DateReturn = dateReturn;
        UserId = userId;
        BookId = bookId;
    }
    
    public Loan(){}
}