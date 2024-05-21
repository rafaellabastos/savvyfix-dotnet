using System.ComponentModel.DataAnnotations;

namespace SavvyFix.Models;

/*
 * Entidade model para armazenar no banco informações das compras
 */

public class Compras
{
    [Key]
    public long IdCompra { get; set; }
    
    public int QntdProd { get; set; }
    public decimal ValorCompra { get; set; }
    
    public long? IdProd { get; set; }

    public string NmProd { get; set; }
}