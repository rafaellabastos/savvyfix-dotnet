using System.ComponentModel.DataAnnotations;

namespace SavvyFix.Models;

public class Compras
{
    [Key]
    public long? IdCliente { get; set; }
    
    public int QntdProd { get; set; }

    public decimal ValorCompra { get; set; }
    
    public long IdCompra { get; set; }

    public long? IdProd { get; set; }

    public long? PrecoVariado { get; set; }

    public string EspecificacaoProd { get; set; } = null!;

    public string NmProd { get; set; } = null!;

    public virtual Clientes? IdClienteNavigation { get; set; }

    public virtual Produtos? IdProdNavigation { get; set; }

    public virtual Atividade? PrecoVariadoNavigation { get; set; }
}