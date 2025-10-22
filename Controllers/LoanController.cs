using LibraryMVC.Infracestruture;
using LibraryMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryMVC.Controllers;

public class LoanController : Controller
{
    private readonly DbAppContext _context;

    public LoanController(DbAppContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.Clients = await _context.Clients.ToListAsync();
        ViewBag.Books = await _context.Books.ToListAsync();

        var loans = await _context.Loans
            .Include(l => l.Client)
            .Include(l => l.Book)
            .ToListAsync();

        return View(loans);
    }

    [HttpPost]
    public async Task<IActionResult> Create([Bind("DateLoan,DateReturn,UserId,BookId")] Loan loan)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _context.Loans.Add(loan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "Error guardando préstamo: " + ex.Message);
            }
        }

        ViewBag.Clients = await _context.Clients.ToListAsync();
        ViewBag.Books = await _context.Books.ToListAsync();

        var loans = await _context.Loans
            .Include(l => l.Client)
            .Include(l => l.Book)
            .ToListAsync();

        return View("Index", loans);
    }


    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var loan = await _context.Loans.FindAsync(id);
        if (loan == null) return NotFound();

        ViewBag.Clients = await _context.Clients.ToListAsync();
        ViewBag.Books = await _context.Books.ToListAsync();

        return View(loan);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, [Bind("Id,DateLoan,DateReturn,UserId,BookId")] Loan loan)
    {
        if (id != loan.Id) return BadRequest();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(loan);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Loans.Any(e => e.Id == loan.Id)) return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewBag.Clients = await _context.Clients.ToListAsync();
        ViewBag.Books = await _context.Books.ToListAsync();

        return View(loan);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var loan = await _context.Loans
            .Include(l => l.Client)
            .Include(l => l.Book)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (loan == null) return NotFound();

        return View(loan);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var loan = await _context.Loans.FindAsync(id);
        if (loan == null) return NotFound();

        _context.Loans.Remove(loan);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
