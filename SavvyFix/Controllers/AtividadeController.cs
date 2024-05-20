using Microsoft.AspNetCore.Mvc;
using SavvyFix.Data;
using SavvyFix.Models;

namespace SavvyFix.Controllers;

public class AtividadeController : Controller
{
    private readonly SavvyFixDbContext _context;

    public AtividadeController(SavvyFixDbContext context)
    {
        _context = context;
    }

    /*
     *  Esse método é apenas para ser um exemplo, a resposta ideal deve ser buscada do cliente de forma
     * automática ao realizar uma compra e utilizar esses dados em uma IA para fornecer uma precificação
     * variada de acordo com as ações dos clientes.
     */

    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> AddAtividade()
    {
        var atividades = new Atividade()
        {
            LocalizacaoAtual = "Av. Paulista, 1106, Bela Vista, São Paulo - SP",
            HorarioAtual = DateTime.Now,
            ClimaAtual = "Frio",
            QntdProcura = 12,
            DemandaProduto = "Al",
            PrecoVariado = 223,
        };

        _context.Add(atividades);
        await _context.SaveChangesAsync();
        
        return RedirectToAction("Index", "Home");
    }
}