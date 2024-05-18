using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SavvyFix.Data;

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
}