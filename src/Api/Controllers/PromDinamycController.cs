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

        public PromDinamycController(IRepository<PromDinamyc> repository)
        {
            _repository = repository;
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
    }
}
