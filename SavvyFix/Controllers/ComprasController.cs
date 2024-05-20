using Microsoft.AspNetCore.Mvc;
using SavvyFix.Data;
using SavvyFix.Models;

namespace SavvyFix.Controllers;

public class ComprasController : Controller
{
    private readonly SavvyFixDbContext _context;

    public ComprasController(SavvyFixDbContext context)
    {
        _context = context;
    }
    
    public IActionResult Index()
    {
        return View();
    }
    
    public async Task<IActionResult> Comprar(long idProd, string nmProd)
    {
        var produto = await _context.Produtos.FindAsync(idProd);
        if (produto == null)
        {
            return NotFound();
        }
        
        var compra = new Compras()
        {
            IdProd = idProd,
            NmProd = nmProd,
            QntdProd = 1,
            ValorCompra = produto.PrecoFixo
             
        };

        return View(compra);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Comprar(Compras compra)
    {
        var produto = await _context.Produtos.FindAsync(compra.IdProd);
        if (produto == null)
        {
            return NotFound();
        }

        compra.ValorCompra = compra.QntdProd * produto.PrecoFixo;
        
        if (ModelState.IsValid)
        {
            _context.Add(compra);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Atividade");
        }

        return View(compra);
    }
}
    
    
    