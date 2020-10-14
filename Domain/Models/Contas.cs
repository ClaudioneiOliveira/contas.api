using System;

namespace contas.api.Domain.Models
{
    public class Contas
    {
        public string Nome { get; set; }
        public double ValorOriginal { get; set; }
        public DateTime Pagamento { get; set; }
    }
}