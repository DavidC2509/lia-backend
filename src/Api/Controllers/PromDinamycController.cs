using Lia.Api.ModelsEntry;
using Lia.Core.PromAggregate;
using Lia.Core.PromDinamicAggregate;
using Lia.SharedKernel.Interface;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Net.Mime;

namespace Lia.Api.Controllers
{
    [ApiController]
    [Route("api/prom-dinamyc")]
    public class PromDinamycController : ControllerBase
    {
        private readonly IRepository<PromDinamyc> _repository;
        private readonly IReadRepository<Prom> _repositoryProm;

        public PromDinamycController(IRepository<PromDinamyc> repository, IReadRepository<Prom> repositoryProm)
        {
            _repository = repository;
            _repositoryProm = repositoryProm;
        }

        [HttpGet("get-list")]
        [SwaggerOperation(OperationId = "ListPromDinamyc", Tags = new[] { "PromDinamyc" })]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PromDinamyc), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<List<PromDinamyc>>> GetListProm(CancellationToken cancellationToken = default)
        {
            return await _repository.ListAsync(cancellationToken);
        }

        [HttpPost("create")]
        [SwaggerOperation(OperationId = "CreatePromDinamyc", Tags = new[] { "PromDinamyc" })]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PromDinamyc), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<PromDinamyc>> CreatePromDinamyc([FromBody] CreatePromDinamycModel data, CancellationToken cancellationToken = default)
        {
            var promDinamyc = new PromDinamyc(data.NameEvent, data.DateEvent, data.CityEvent, data.AddresEvent, data.AdditionalInformation);
            await _repository.AddAsync(promDinamyc, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            return Ok(promDinamyc);
        }

        [HttpPost("{id}/create-prom")]
        [SwaggerOperation(OperationId = "CreatePromFromPromDinamyc", Tags = new[] { "PromDinamyc" })]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PromDinamyc), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<PromDinamyc>> CreatePromFromPromDinamyc([FromRoute] string idPromDinamyc, [FromBody] CreatePromFromDinamyc data, CancellationToken cancellationToken = default)
        {
            var prom = await _repositoryProm.GetByIdAsync(data.PromID);
            var promDinamyc = await _repository.GetByIdAsync(idPromDinamyc);
            var textProm = ReplacePlaceholders(prom!, promDinamyc!);
            promDinamyc!.PromModified = textProm;
            await _repository.UpdateAsync(promDinamyc, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            return Ok(promDinamyc);
        }

        [HttpPost("{id}/up-assistan")]
        [SwaggerOperation(OperationId = "PromDinamycUpToAssistan", Tags = new[] { "PromDinamyc" })]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PromDinamyc), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<bool>> PromDinamycUpToAssistan([FromRoute] string idPromDinamyc, [FromBody] string nameAssistan, CancellationToken cancellationToken = default)
        {
            var prom = await _repository.GetByIdAsync(idPromDinamyc);

            return Ok(true);
        }

        public static string ReplacePlaceholders(Prom prom, PromDinamyc promDinamyc)
        {
            return prom.PromModified
                   .Replace("{Nombre_Evento}", promDinamyc.NameEvent)
                   .Replace("{Ciudad_Evento}", promDinamyc.CityEvent)
                   .Replace("{Ubicacion_Evento}", promDinamyc.AddresEvent)
                   .Replace("{Informacion_Adicional}", promDinamyc.AdditionalInformation);
        }
    }
}
