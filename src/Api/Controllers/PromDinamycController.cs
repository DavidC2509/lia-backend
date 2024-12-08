using Lia.Api.ModelsEntry;
using Lia.Core.Interface;
using Lia.Core.Models.OpenIa;
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
        private readonly IVirtualAssistant _serviceVirtualAssistan;

        public PromDinamycController(IRepository<PromDinamyc> repository, IReadRepository<Prom> repositoryProm, IVirtualAssistant serviceVirtualAssistan)
        {
            _repository = repository;
            _repositoryProm = repositoryProm;
            _serviceVirtualAssistan = serviceVirtualAssistan;
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
            var promDinamyc = new PromDinamyc(data.NameEvent, data.DateEvent, data.CityEvent, data.AddresEvent, data.AdditionalInformation, data.NameClient, data.PackageTitle, data.CountNigthsHotel, data.Vigency);
            await _repository.AddAsync(promDinamyc, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            return Ok(promDinamyc);
        }

        [HttpPost("{idPromDinamyc}/create-prom")]
        [SwaggerOperation(OperationId = "CreatePromFromPromDinamyc", Tags = new[] { "PromDinamyc" })]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PromDinamyc), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<PromDinamyc>> CreatePromFromPromDinamyc([FromRoute] Guid idPromDinamyc, [FromBody] CreatePromFromDinamyc data, CancellationToken cancellationToken = default)
        {
            var prom = await _repositoryProm.GetByIdAsync(data.PromID);
            var promDinamyc = await _repository.GetByIdAsync(idPromDinamyc);
            var textProm = ReplacePlaceholders(prom!, promDinamyc!);
            promDinamyc!.PromModified = textProm;
            await _repository.UpdateAsync(promDinamyc, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            return Ok(promDinamyc);
        }

        [HttpPost("{idPromDinamyc}/up-assistan")]
        [SwaggerOperation(OperationId = "PromDinamycUpToAssistan", Tags = new[] { "PromDinamyc" })]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PromDinamyc), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ResponseStoreAssistanIa>> PromDinamycUpToAssistan([FromRoute] Guid idPromDinamyc, [FromBody] UpAssistantProm data, CancellationToken cancellationToken = default)
        {
            var prom = await _repository.GetByIdAsync(idPromDinamyc);
            if (string.IsNullOrEmpty(prom?.PromModified)) return BadRequest("Need Text PromDinamyc");
            var request = new RequestUpAssistan(data.NameFile, prom.PromModified, data.ModelIa, data.ToolsIa);
            return await _serviceVirtualAssistan.UpAssistan(request, cancellationToken);
        }

        public static string ReplacePlaceholders(Prom prom, PromDinamyc promDinamyc)
        {
            return prom.PromModified
                   .Replace("{Nombre_Evento}", promDinamyc.NameEvent)
                   .Replace("{Ciudad_Evento}", promDinamyc.CityEvent)
                   .Replace("{Ubicacion_Evento}", promDinamyc.AddresEvent)
                   .Replace("{Informacion_Adicional}", promDinamyc.AdditionalInformation)
                   .Replace("{Nombre_Cliente}", promDinamyc.NameClient)
                   .Replace("{TITULO_DEL_PAQUETE}", promDinamyc.PackageTitle)
                   .Replace("{Cant_Noches_Hotel}", promDinamyc.CountNigthsHotel.ToString())
                   .Replace("{Vigencia}", promDinamyc.Vigency);

        }
    }
}
