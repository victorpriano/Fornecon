using System.ComponentModel.DataAnnotations;

namespace Fornecon.WebAPI.Models
{
    public class Fornecedor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }
        public string NomeFantasia { get; set; }

        [Required]
        [StringLength(14, ErrorMessage = "Deve possuir até 14 caracteres")]
        public string CNPJ { get; set; }
        public bool Ativo { get; set; }
    }
}
