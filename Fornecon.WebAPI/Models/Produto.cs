using System.ComponentModel.DataAnnotations;

namespace Fornecon.WebAPI.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }
        public int IdFornecedor { get; set; }

        [Required]
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInclusao { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [Required]
        public decimal Preco { get; set; }
        public bool Ativo { get; set; }
        public Fornecedor Fornecedor { get; set; }
    }
}
