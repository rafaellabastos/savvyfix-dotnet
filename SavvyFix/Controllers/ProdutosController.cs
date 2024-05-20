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
    /*
     *  Buscar todos os produtos registrados no banco
     */
    public async Task<IActionResult> Index()
    {
        return View(await _context.Produtos.ToListAsync());
    }

    /*
     *  Tela para o método de adicionar produtos
     */
    
    public IActionResult Adicionar()
    {
        return View();
    }
    
    /*
     *  Método POST para adicionar um novo produto ao banco  
     */
    
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
    
    /*
     *  Tela para editar produtos salvos
     */
    
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
    
    /*
     *  Método UPDATE para editar produtos através de um POST
     */
    
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
    
    /*
     * Verificação de existência do produto pelo id passado pelo endpoint
     */
    
    private bool ProdutoExists(long id)
    {
        return _context.Produtos.Any(e => e.IdProd == id);
    }
    
    /*
     * Tela para excluir os produtos
     */
    public async Task<IActionResult> Excluir(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }
    
        var produtos = await _context.Produtos
            .FirstOrDefaultAsync(m => m.IdProd == id);
        if (produtos == null)
        {
            return NotFound();
        }
    
        return View(produtos);
    }
    
    /*
     * Método DELETE para excluir produto do banco 
     */
    
    [HttpPost, ActionName("Excluir")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ExcluirConfirmado(long id)
    {
        if (id == 0)
        {
            return NotFound();
        }
        var produtos = await _context.Produtos.FindAsync(id);
        if (produtos != null)
        {
            _context.Produtos.Remove(produtos);
            await _context.SaveChangesAsync();
        }
        else
        {
            Console.WriteLine($"produto: {produtos} id: {id}");
        }
        return RedirectToAction("Index", "Produtos");
    }
    
}