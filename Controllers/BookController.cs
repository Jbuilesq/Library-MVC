using LibraryMVC.Infracestruture;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMVC.Controllers;

public class BookController : Controller
{
    private readonly DbAppContext _context;

    public BookController(DbAppContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }
}