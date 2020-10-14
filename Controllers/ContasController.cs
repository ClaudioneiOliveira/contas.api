using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using contas.api.Domain.Service;
using contas.api.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace contas.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContasController : ControllerBase
    {
        private readonly ILogger<ContasController> _logger;
        private readonly IContasService contasService;

        public ContasController(
            ILogger<ContasController> logger,
            IContasService contasService)
        {
            this._logger = logger;
            this.contasService = contasService;
        }

        [HttpPost("add")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ContasListagem AddContas([FromBody] ContasInclusao conta)
            => contasService.AdicionarContas(conta);

        [HttpGet("list")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IEnumerable<ContasListagem> GetContas()
            => contasService.ListarContas();
    }
}