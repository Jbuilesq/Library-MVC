using Microsoft.EntityFrameworkCore;
using LibraryMVC.Models;

namespace LibraryMVC.Infracestruture;

public class DbAppContext : DbContext
{
    public DbAppContext(DbContextOptions<DbAppContext> options) : base(options)
    {
        
    }
    
    public DbSet<Book> Books { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Loan> Loans { get; set; }

  
}