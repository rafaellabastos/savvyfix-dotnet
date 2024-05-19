using System.ComponentModel.DataAnnotations;

namespace SavvyFix.Models;

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