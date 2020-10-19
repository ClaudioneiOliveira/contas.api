using System.Collections.Generic;
using contas.api.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using contas.api.Domain.Models.Exceptions;
using contas.api.Service;
using System.ComponentModel.DataAnnotations;

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
        [ProducesResponseType(typeof(IEnumerable<ErrorMessage>), 400)]
        [ProducesResponseType(typeof(IEnumerable<ErrorMessage>), 500)]
        public IActionResult AddContas([FromBody][Required] ContasInclusao conta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            else
                return StatusCode(200, contasService.AdicionarContas(conta));
        }

        [HttpGet("list")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(IEnumerable<ErrorMessage>), 400)]
        [ProducesResponseType(typeof(IEnumerable<ErrorMessage>), 500)]
        public IEnumerable<ContasListagem> GetContas()
            => contasService.ListarContas();
    }
}