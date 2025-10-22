using LibraryMVC.Infracestruture;
using LibraryMVC.Models;
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
    
    // Create is the method to create new client in our database 
    [HttpPost]
    public async Task<IActionResult> Create([Bind("CodeId,Title,Author,NumberOfShares")] Book book)
    {
       
        if (ModelState.IsValid)
        {
            // Add the new book to the context
            _context.Books.Add(book);

            // Saves the changes on the DB (INSERT)
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(book);
    }
    
    private bool BookExist(string codeId)
    {
        return _context.Books.Any(e => e.CodeId == codeId);
    }
    
    
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) NotFound();
        return View(book);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(int id, [Bind("Id,CodeId,Title,Author,NumberOfShares")] Book book)
    {
        if (id != book.Id) BadRequest();
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(book);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExist(book.CodeId)) return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(book);
    }
    
    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
    
        var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
        if (book == null) return NotFound();

        return View(book);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book != null)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync(); // DELETE
        }

        return RedirectToAction(nameof(Index));
    }

    
    
}