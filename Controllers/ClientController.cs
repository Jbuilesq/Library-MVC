using LibraryMVC.Infracestruture;
using LibraryMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryMVC.Controllers;

public class ClientController : Controller
{
    // It is to call de database and use in this controller
    private readonly DbAppContext _context;

    public ClientController(DbAppContext context)
    { 
        _context = context;
    }

    //Index is the method to show the clients in the client Index
    public async Task<IActionResult> Index()
    {
        var clients = await _context.Clients.ToListAsync();
        return View(clients);
    }


    // Create is the method to create new client in our database 
    [HttpPost]
    public async Task<IActionResult> Create([Bind("Name,Email,DNI,Phone")] Client client)
    {
        // if (EmailExist(client.Email))
        // {
        //     ModelState.AddModelError("Email", "El email ya existe");
        //     return View(client);
        // }
        // if (DNIExist(client.DNI))
        // {
        //     ModelState.AddModelError("DNI", "El DNI ya existe");
        //     return View(client);
        // } 
        if (ModelState.IsValid)
        {
            // Add the new client to the context
            _context.Clients.Add(client);

            // Saves the changes on the DB (INSERT)
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(client);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client == null) NotFound();
        return View(client);
    }

    private bool ClientExist(int id)
    {
        return _context.Clients.Any(e => e.Id == id);
    }

    private bool EmailExist(string email)
    {
        return _context.Clients.Any(e => e.Email == email);
    }

    private bool DNIExist(string dni)
    {
        return _context.Clients.Any(e => e.DNI == dni);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,DNI,Phone")] Client client)
    {
        if (id != client.Id) BadRequest();
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(client);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExist(client.Id)) return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(client);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client == null) NotFound();
        return View(client);
    }


    public async Task<IActionResult> Delete(int id)
    {
        var client = await _context.Clients.FindAsync(id);

        if (client != null)
        {
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync(); // (DELETE)
        }

        return RedirectToAction(nameof(Index));
    }
}