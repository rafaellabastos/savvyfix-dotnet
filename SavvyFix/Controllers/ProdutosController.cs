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
        //Console.WriteLine($"CpfClie: {clientes.CpfClie}, NmClie: {clientes.NmClie}, SenhaClie: {clientes.SenhaClie}");
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
}