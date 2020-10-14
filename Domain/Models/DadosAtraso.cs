namespace contas.api.Domain.Models
{
    public class DadosAtraso
    {
        public double Valor { get; set; }
        public int DiasAtraso { get; set; }
        public double Multa { get; set; }
        public double Juros { get; set; }
    }
}