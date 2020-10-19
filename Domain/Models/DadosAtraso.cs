namespace contas.api.Domain.Models
{
    public class DadosAtraso
    {
        public decimal Valor { get; set; }
        public int DiasAtraso { get; set; }
        public decimal Multa { get; set; }
        public decimal Juros { get; set; }
    }
}