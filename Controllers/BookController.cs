using LibraryMVC.Infracestruture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryMVC.Controllers;

public class BookController : Controller
{
    private readonly DbAppContext _context;

    public BookController(DbAppContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var books = await _context.Books.ToListAsync();
        return View(books);
    }
}