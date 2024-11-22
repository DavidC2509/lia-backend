using Lia.Core.PromAggregate;
using Lia.SharedKernel.Interface;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Net.Mime;

namespace Lia.Api.Controllers
{
    [ApiController]
    [Route("api/prom")]
    public class PromController : ControllerBase
    {
        private readonly IRepository<Prom> _repository;

        public PromController(IRepository<Prom> repository)
        {
            _repository = repository;
        }

        [HttpGet("get-list")]
        [SwaggerOperation(OperationId = "ListProm", Tags = new[] { "Prom" })]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Prom), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<List<Prom>>> GetListProm(CancellationToken cancellationToken = default)
        {
            return await _repository.ListAsync(cancellationToken);
        }

        [HttpPost("create")]
        [SwaggerOperation(OperationId = "CreateProm", Tags = new[] { "Prom" })]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Prom), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Prom>> CreateProm([FromBody] string promt, CancellationToken cancellationToken = default)
        {
            var prom = new Prom(promt);
            await _repository.AddAsync(prom, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            return Ok(prom);
        }
    }
}
