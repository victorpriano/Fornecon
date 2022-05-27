using System.ComponentModel.DataAnnotations;

namespace Fornecon.WebAPI.Models
{
    public class Contrato
    {
        [Key]
        public int Id { get; set; }
        public int IdFornecedor { get; set; }
        public DateTime DataVencimento { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "O valor deve possuir 10 caracteres")]
        public string DocContrato { get; set; }
        public bool Ativo { get; set; }
        public Fornecedor Fornecedor { get; set; }
    }
}
