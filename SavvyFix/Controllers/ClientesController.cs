using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SavvyFix.Data;
using SavvyFix.Models;

namespace SavvyFix.Controllers;

public class ClientesController : Controller
{
    private readonly SavvyFixDbContext _context;

    public ClientesController(SavvyFixDbContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> Index()
    {
        return View(await _context.Clientes.ToListAsync());
    }
    
    public async Task<IActionResult> Cadastrados(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var boardgame = await _context.Clientes
            .FirstOrDefaultAsync(m => m.IdCliente == id);
        if (boardgame == null)
        {
            return NotFound();
        }

        return View(boardgame);
    }
    
    public IActionResult Cadastrar()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cadastrar([Bind("IdEndereco,CpfClie,NmClie,Senha")]  Clientes clientes)
    {
        if (ModelState.IsValid)
        {
            _context.Add(clientes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(clientes);
    }
    
    
}