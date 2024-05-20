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
    
    public IActionResult Cadastrar()
    {
        return View();
    }
    
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
                return RedirectToAction("Index", "Home");
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

    public IActionResult Login()
    {
        return View();
    }
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
}