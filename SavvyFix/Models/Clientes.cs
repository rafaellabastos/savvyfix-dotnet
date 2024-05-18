using System.ComponentModel.DataAnnotations;

namespace SavvyFix.Models;

public class Clientes

{
    [Key]
    public long IdCliente { get; set; }
    

    public long? IdEndereco { get; set; }

    public string CpfClie { get; set; } = null!;

    public string NmClie { get; set; } = null!;

    public string SenhaClie { get; set; } = null!;
    
    public virtual Atividade? Atividade { get; set; }

    public virtual ICollection<Compras> Compras { get; set; } = new List<Compras>();

    public virtual Enderecos? IdEnderecoNavigation { get; set; }

    
}