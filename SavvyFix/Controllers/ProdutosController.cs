using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SavvyFix.Data;
using SavvyFix.Models;

namespace SavvyFix.Controllers;

public class ProdutosController : Controller
{
    private readonly SavvyFixDbContext _context;

    public ProdutosController(SavvyFixDbContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> Index()
    {
        return View(await _context.Produtos.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var produtos = _context.Produtos.FirstOrDefault(m => m.IdProd == id);

        if (produtos == null)
        {
            return NotFound();
        }

        return View(produtos);
    }

    public IActionResult Adicionar()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Adicionar([Bind("NmProd, PrecoFixo, MarcaProd, DescProd, Img" )]  Produtos produtos)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _context.Add(produtos);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Produtos");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }else
        {
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    Console.WriteLine($"ModelState Error: {error.ErrorMessage}");
                }
            }
        }
        return View(produtos);
    }
    
    public async Task<IActionResult> Editar(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var produtos = await _context.Produtos.FindAsync(id);
        if (produtos == null)
        {
            return NotFound();
        }
        return View(produtos);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(int id, [Bind("IdProd, NmProd, PrecoFixo, MarcaProd, DescProd, Img")] Produtos produtos)
    {
        if (id != produtos.IdProd)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(produtos);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(produtos.IdProd))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(produtos);
    }
    
    private bool ProdutoExists(long id)
    {
        return _context.Produtos.Any(e => e.IdProd == id);
    }
}