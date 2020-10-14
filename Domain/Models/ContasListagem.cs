namespace contas.api.Domain.Models
{
    public class ContasListagem : Contas
    {
        public double ValorCorrigido { get; set; }
        public int DiasAtraso { get; set; }
    }
}