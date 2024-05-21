using System.ComponentModel.DataAnnotations;

namespace SavvyFix.Models;

/*
 * Entidade model para armazenar no banco de dados as informações dos produtos da loja
 */

public class Produtos
{ 
    [Key]
    public long IdProd { get; set; }
    
    [Required]
    public decimal PrecoFixo { get; set; }
    
    public string MarcaProd { get; set; } = null!;

    public string DescProd { get; set; } = null!;

    public string NmProd { get; set; } = null!;
    
    public string Img { get; set; }
}