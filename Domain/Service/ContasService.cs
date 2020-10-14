using System;
using System.Collections.Generic;
using contas.api.Domain.Service;
using contas.api.Domain.Models;
using contas.api.Domain.Extensions;

namespace contas.api.Service
{
    public class ContasService : IContasService
    {
        public ContasListagem AdicionarContas(ContasInclusao conta)
        {
            try
            {
                var retorno = ContasListagemMap(conta);
                if (retorno.DiasAtraso > 0)
                {
                    var dados = new DadosAtraso
                    {
                        DiasAtraso = retorno.DiasAtraso,
                        Valor = conta.ValorOriginal
                    };

                    switch (dados.DiasAtraso)
                    {
                        case int n when (n > 0 && n <= 3):
                            dados.Juros = 0.1D;
                            dados.Multa = 2D;
                            break;
                        case int n when (n > 3 && n < 5):
                            dados.Juros = 0.2D;
                            dados.Multa = 3D;
                            break;
                        case int n when (n > 5):
                            dados.Juros = 0.3D;
                            dados.Multa = 5D;
                            break;
                        default:
                            break;
                    }
                    retorno.ValorCorrigido = this.Calcular(dados);
                }


                return retorno;
            }
            catch (Exception)
            {
                return null;
            }
        }

        IEnumerable<ContasListagem> IContasService.ListarContas()
        {
            return new List<ContasListagem>();
        }

        private double Calcular(DadosAtraso dados)
        {
            var mora = ((dados.DiasAtraso * dados.Juros / 100) * dados.Valor).Rnd2();
            var multa = (dados.Valor * (dados.Multa / 100)).Rnd2();
            return (dados.Valor + mora + multa).Rnd2();
        }

        private ContasListagem ContasListagemMap(ContasInclusao conta)
        {
            var ret = new ContasListagem{
                Nome = conta.Nome,
                Pagamento = conta.Pagamento,
                ValorOriginal = conta.ValorOriginal,
                DiasAtraso = conta.Pagamento.Subtract(conta.Vencimento).Days > 0 ? conta.Pagamento.Subtract(conta.Vencimento).Days : 0,
                ValorCorrigido = 0D
            };
            return ret;
        }
    }
}