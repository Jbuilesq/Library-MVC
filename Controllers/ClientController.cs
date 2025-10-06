using LibraryMVC.Infracestruture;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMVC.Controllers;

public class ClientController : Controller
{   
    
    // It is to call de database and use in this controller
    private readonly DbAppContext _context;
    
    public ClientController(DbAppContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    
}