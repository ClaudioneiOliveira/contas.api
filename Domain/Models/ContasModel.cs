using System;

namespace contas.api.Domain.Models
{
    public partial class ContasModel
    {
        public decimal CodSequencia { get; set; }
        public string Nome { get; set; }
        public decimal ValorOriginal { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal? ValorCorrigido { get; set; }
        public int? DiasAtraso { get; set; }
        public decimal? PercJuros { get; set; }
        public decimal? PercMulta { get; set; }
        public decimal? ValorJuros { get; set; }
        public decimal? ValorMulta { get; set; }
    }
}