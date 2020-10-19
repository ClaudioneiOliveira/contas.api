using System;

namespace contas.api.Domain.Models
{
    public class ContasListagem
    {
        public decimal id { get; set; }
        public string Nome { get; set; }
        public decimal ValorOriginal { get; set; }
        public decimal ValorCorrigido { get; set; }
        public int DiasAtraso { get; set; }
        public DateTime DataPagamento { get; set; }
    }
}