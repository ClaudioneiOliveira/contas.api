using System;
using System.ComponentModel.DataAnnotations;

namespace contas.api.Domain.Models
{
    public class ContasInclusao
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo ValorOriginal é obrigatório.")]
        [Range(1, 9999999)]
        [DataType(DataType.Currency)]
        public decimal ValorOriginal { get; set; }

        [Required(ErrorMessage = "O campo Pagamento é obrigatório.")]
        public string Pagamento { get; set; }

        [Required(ErrorMessage = "O campo Vencimento é obrigatório.")]
        public string Vencimento { get; set; }
    }
}