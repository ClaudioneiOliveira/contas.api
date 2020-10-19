using System;
using System.Collections.Generic;
using contas.api.Domain.Models;
using contas.api.Domain.Extensions;
using contas.api.Infrastructure.Context;
using System.Linq;
using contas.api.Domain.Models.Exceptions;

namespace contas.api.Service
{
    public class ContasService : IContasService
    {
        private readonly ContasContext _context;

        public ContasService(ContasContext context)
        {
            _context = context;
        }

        public ContasListagem AdicionarContas(ContasInclusao conta)
        {
            var dados = new DadosAtraso();
            var calculo = new DadosAtrasoRetorno();
            var retorno = ContasListagemMap(conta);
            if (retorno.DiasAtraso > 0)
            {
                dados = new DadosAtraso
                {
                    DiasAtraso = retorno.DiasAtraso,
                    Valor = conta.ValorOriginal
                };

                switch (dados.DiasAtraso)
                {
                    case int n when (n > 0 && n <= 3):
                        dados.Juros = 0.1M;
                        dados.Multa = 2M;
                        break;
                    case int n when (n > 3 && n < 5):
                        dados.Juros = 0.2M;
                        dados.Multa = 3M;
                        break;
                    case int n when (n > 5):
                        dados.Juros = 0.3M;
                        dados.Multa = 5M;
                        break;
                    default:
                        break;
                }
                calculo = this.Calcular(dados);
                retorno.ValorCorrigido = calculo.ValorCorrigido;
            }
            else
            {
                dados.Valor = conta.ValorOriginal;
                retorno.ValorCorrigido = conta.ValorOriginal;
            }
            var contaModel = ContasMap(retorno);
            contaModel.DataVencimento = DateTime.Parse(conta.Vencimento);
            contaModel.PercJuros = dados.Juros;
            contaModel.PercMulta = dados.Multa;
            contaModel.ValorJuros = calculo.Juros;
            contaModel.ValorMulta = calculo.Multa;
            _context.Contas.Add(contaModel);
            _context.SaveChanges();
            return retorno;
        }

        public IEnumerable<ContasListagem> ListarContas()
        {
            return _context.Contas.Select(x => new ContasListagem
            {
                id = x.CodSequencia,
                DiasAtraso = x.DiasAtraso.Value,
                Nome = x.Nome,
                DataPagamento = x.DataPagamento,
                ValorCorrigido = x.ValorCorrigido.Value,
                ValorOriginal = x.ValorOriginal
            }).ToList();
        }

        private DadosAtrasoRetorno Calcular(DadosAtraso dados)
        {
            return new DadosAtrasoRetorno
            {
                Juros = ((dados.DiasAtraso * dados.Juros / 100M) * dados.Valor).Rnd2(),
                Multa = (dados.Valor * (dados.Multa / 100M)).Rnd2(),
                Valor = dados.Valor
            };
        }

        private ContasListagem ContasListagemMap(ContasInclusao conta)
        {
            var ret = new ContasListagem
            {
                Nome = conta.Nome,
                DataPagamento = DateTime.Parse(conta.Pagamento),
                ValorOriginal = conta.ValorOriginal,
                DiasAtraso = DateTime.Parse(conta.Pagamento).Subtract(DateTime.Parse(conta.Vencimento)).Days > 0 ? DateTime.Parse(conta.Pagamento).Subtract(DateTime.Parse(conta.Vencimento)).Days : 0,
                ValorCorrigido = 0M
            };
            return ret;
        }

        private ContasModel ContasMap(ContasListagem conta)
        {
            var ret = new ContasModel
            {
                Nome = conta.Nome,
                DataPagamento = conta.DataPagamento,
                ValorOriginal = conta.ValorOriginal,
                DiasAtraso = conta.DiasAtraso,
                ValorCorrigido = conta.ValorCorrigido,
            };
            return ret;
        }
    }
}