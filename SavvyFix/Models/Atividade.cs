using System.ComponentModel.DataAnnotations;

namespace SavvyFix.Models;

public class Atividade
{
    [Key]
    public long IdAtividades { get; set; }

    public string? ClimaAtual { get; set; }

    public string DemandaProduto { get; set; }

    public DateTime? HorarioAtual { get; set; }

    public string? LocalizacaoAtual { get; set; }

    public decimal PrecoVariado { get; set; }

    public int QntdProcura { get; set; }
}