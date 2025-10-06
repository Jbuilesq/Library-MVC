using System.ComponentModel.DataAnnotations;

namespace LibraryMVC.Models;

public class Client
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Email { get; set; }
    [Required]
    public string DNI { get; set; }
    public string Phone { get; set; }
    public ICollection<Loan> loans { get; set; } = new List<Loan>();
    
    public Client(string name, string dni, string email = null, string phone = null)
    {
        Name = name;
        DNI = dni;
        Email = email;
        Phone = phone;
    }

    public Client(){}

}