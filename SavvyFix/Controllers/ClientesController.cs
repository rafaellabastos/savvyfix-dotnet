using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
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
    
    /*
     *  Tela para o método de buscar clientes pelo cpf
     */
    
    public async Task<IActionResult> Index(string cpf)
    {
        var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.CpfClie == cpf);
        
        if (cliente == null)
        {
            return NotFound();
        }

        var usuario = new Clientes()
        {
            IdCliente = cliente.IdCliente,
            CpfClie = cliente.CpfClie,
            NmClie = cliente.NmClie,
            CepEndereco = cliente.CepEndereco
        };
        
        return View(usuario);
    }
    
    /*
     *  Tela para o método de cadastar novos clientes pelo id pelo GET
     */
    
    public async Task<IActionResult> Cadastrados(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var clientes = await _context.Clientes
            .FirstOrDefaultAsync(m => m.IdCliente == id);
        if (clientes == null)
        {
            return NotFound();
        }

        return View(clientes);
    }
    
    /*
     *  Tela para o método de cadastrar clientes
     */
    
    public IActionResult Cadastrar()
    {
        return View();
    }
    
    /*
     *  Método POST para cadastrar novos clientes ao banco
     */
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cadastrar([Bind("CpfClie,NmClie,SenhaClie,CepEndereco, RuaEndereco, NumEndereco")]  Clientes clientes)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _context.Add(clientes);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "Clientes");
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
        return View(clientes);
    }

    /*
     *  Tela para o método de login 
     */
    
    public IActionResult Login()
    {
        return View();
    }
    
    /*
     * Método POST para validar clientes registrados no banco
     */
    
    [HttpPost]
    public async Task<IActionResult> Login(string cpf, string senha)
    {
        var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.CpfClie == cpf && c.SenhaClie == senha);

        if (cliente != null)
        {
            return RedirectToAction("Index", "Clientes", new {cpf = cliente.CpfClie});
        }
        else
        {
            ViewBag.ErrorMessage = "Credenciais inválidas. Por favor, tente novamente.";
            return View("Login");
        }
    }
    
    /*
     *  Método GET para buscar o cliente pelo id
     */
    
    public async Task<IActionResult> Editar(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente == null)
        {
            return NotFound();
        }
        return View(cliente);
    }
    
    /*
     *  Método UPDATE para editar produtos através de um POST
     */
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(int id, [Bind("IdCliente, CpfClie, NmClie, SenhaClie, CepEndereco, RuaEndereco, NumEndereco")] Clientes clientes)
    {
        if (id != clientes.IdCliente)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(clientes);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(clientes.IdCliente))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Login", "Clientes");
        }
        return View(clientes);
    }
    
    /*
     *  Verificar se o cliente existe pelo id passado no endpoint 
     */
    
    private bool ClienteExists(long id)
    {
        return _context.Clientes.Any(e => e.IdCliente == id);
    }
    
    /*
     *  Tela para o método de excluir cadastro usando GET pelo id
     */
    
    public async Task<IActionResult> Excluir(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }
    
        var clientes = await _context.Clientes
            .FirstOrDefaultAsync(m => m.IdCliente == id);
        if (clientes == null)
        {
            return NotFound();
        }
    
        return View(clientes);
    }
    
    /*
     * Método DELETE para excluir cadastros
     */
    
    [HttpPost, ActionName("Excluir")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ExcluirConfirmado(long id)
    {
        if (id == 0)
        {
            return NotFound();
        }
        var clientes = await _context.Clientes.FindAsync(id);
        if (clientes != null)
        {
            _context.Clientes.Remove(clientes);
            await _context.SaveChangesAsync();
        }
        else
        {
            Console.WriteLine($"produto: {clientes} id: {id}");
        }
        return RedirectToAction("Index", "Home");
    }
}