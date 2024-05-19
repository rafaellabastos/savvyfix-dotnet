using System.ComponentModel.DataAnnotations;

namespace SavvyFix.Models;

public class Compras
{
    [Key]
    public long IdCompra { get; set; }
    
    public int QntdProd { get; set; }
    public decimal ValorCompra { get; set; }
    
    public long? IdProd { get; set; }

    public string NmProd { get; set; }
}