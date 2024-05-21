using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    
    /*
     *  Buscar todas as compras registradas no banco
     */
    public async Task<IActionResult> Index()
    {
        return View(await _context.Compra.ToListAsync());
    }
    
    /*
     *  Tela para o método de adicionar compras ao banco de dados
     */
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

    /*
     *  Método POST para adicionar uma nova compra ao banco
     */
    
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
    
    /*
     *  Tela para editar a compra no histórico salva
     */
    
    public async Task<IActionResult> Editar(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var compras = await _context.Compra.FindAsync(id);
        if (compras == null)
        {
            return NotFound();
        }
        return View(compras);
    }
    
    /*
     *  Método UPDATE para editar o histórico de compras através de um POST
     */
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(int id, [Bind("IdCompra, NmProd, QntdProd, ValorCompra")] Compras compras)
    {
        if (id != compras.IdCompra)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(compras);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompraExists(compras.IdCompra))
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
        return View(compras);
    }
    
    /*
     * Verificação de existência da compra pelo id passado pelo endpoint
     */
    
    private bool CompraExists(long id)
    {
        return _context.Compra.Any(e => e.IdCompra == id);
    }
    
    /*
     * Tela para excluir os históricos de compras
     */
    public async Task<IActionResult> Excluir(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }
    
        var compras = await _context.Compra
            .FirstOrDefaultAsync(m => m.IdCompra == id);
        if (compras == null)
        {
            return NotFound();
        }
    
        return View(compras);
    }
    
    /*
     * Método DELETE para excluir histórico do banco de dados
     */
    
    [HttpPost, ActionName("Excluir")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ExcluirConfirmado(long id)
    {
        if (id == 0)
        {
            return NotFound();
        }
        var compras = await _context.Compra.FindAsync(id);
        if (compras != null)
        {
            _context.Compra.Remove(compras);
            await _context.SaveChangesAsync();
        }
        else
        {
            Console.WriteLine($"produto: {compras} id: {id}");
        }
        return RedirectToAction("Index", "Compras");
    }
    
}
    
    
    