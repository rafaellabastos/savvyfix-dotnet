using System.ComponentModel.DataAnnotations;

namespace SavvyFix.Models;

public class Enderecos
{
    [Key]
    public long IdEndereco { get; set; }
    
    public string EstadoEndereco { get; set; } = null!;

    public string CepEndereco { get; set; } = null!;


    public string NumEndereco { get; set; } = null!;

    public string BairroEndereco { get; set; } = null!;

    public string CidadeEndereco { get; set; } = null!;

    public string RuaEndereco { get; set; } = null!;

    public string Pais { get; set; } = null!;

    public virtual ICollection<Clientes> Clientes { get; set; } = new List<Clientes>();
}