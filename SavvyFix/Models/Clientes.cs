using System.ComponentModel.DataAnnotations;

namespace SavvyFix.Models
{
    public class Clientes
    {
        [Key]
        public long IdCliente { get; set; }

        public long? IdEndereco { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        public string CpfClie { get; set; } = null!;

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string NmClie { get; set; } = null!;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string SenhaClie { get; set; } = null!;
        
        public virtual Atividade? Atividade { get; set; }

        public virtual ICollection<Compras> Compras { get; set; } = new List<Compras>();

        public virtual Enderecos? IdEnderecoNavigation { get; set; }
    }
}