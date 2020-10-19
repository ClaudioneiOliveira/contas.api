using System.Collections.Generic;
using contas.api.Domain.Models;

namespace contas.api.Service
{
    public interface IContasService
    {
        ContasListagem AdicionarContas(ContasInclusao conta);
        IEnumerable<ContasListagem> ListarContas();
    }
}