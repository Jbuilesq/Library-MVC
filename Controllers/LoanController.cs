using LibraryMVC.Infracestruture;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMVC.Controllers;

public class LoanController : Controller
{
    public readonly DbAppContext _context;

    public LoanController(DbAppContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }
}