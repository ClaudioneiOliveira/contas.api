using System;

namespace contas.api.Domain.Models
{
    public class ContasInclusao : Contas
    {
        public DateTime Vencimento { get; set; }
    }
}