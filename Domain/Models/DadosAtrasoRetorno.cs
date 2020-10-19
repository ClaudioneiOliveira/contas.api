using contas.api.Domain.Extensions;

namespace contas.api.Domain.Models
{
    public class DadosAtrasoRetorno
    {
        public decimal Valor { get; set; }
        public decimal Multa { get; set; }
        public decimal Juros { get; set; }
        public decimal ValorCorrigido
        {
            get => (this.Valor + this.Juros + this.Multa).Rnd2();
        }
    }
}