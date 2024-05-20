using System.ComponentModel.DataAnnotations;

namespace SavvyFix.Models
{
    /*
     * Entidade model para armazenar no banco de dados as informações dos usuários cadastrados
     */
    
    public class Clientes
    {
        [Key]
        public long IdCliente { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter exatamente 11 números.")]
        public string CpfClie { get; set; } = null!;

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string NmClie { get; set; } = null!;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string SenhaClie { get; set; } = null!;

        [Required(ErrorMessage = "O cep é obrigatório.")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "O CEP deve ter exatamente 8 números.")]
        public string CepEndereco { get; set; }

        [Required(ErrorMessage = "A rua é obrigatória.")]
        public string RuaEndereco { get; set; }
        
        [Required(ErrorMessage = "O número é obrigatório.")]
        public string NumEndereco { get; set; }
        
        
    }
}