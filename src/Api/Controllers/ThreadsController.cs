using Lia.Core.Interface;
using Lia.Core.Models.Threads;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Net.Mime;

namespace Lia.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class ThreadsController : ControllerBase
    {
        private readonly IVirtualAssistant _serviceVirtualAssistan;

        public ThreadsController(IVirtualAssistant serviceVirtualAssistan)
        {
            _serviceVirtualAssistan = serviceVirtualAssistan;
        }

        [HttpGet("create-threads")]
        [SwaggerOperation(OperationId = "CreateThreads", Tags = new[] { "ApiLia" })]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseCreateThreads), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ResponseCreateThreads>> CreateThreads(CancellationToken cancellationToken = default)
        {
            return await _serviceVirtualAssistan.CreateThreads(cancellationToken);
        }

        [HttpPost("add-message-threads/{id}")]
        [SwaggerOperation(OperationId = "AddMessages", Tags = new[] { "ApiLia" })]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseAddMessageThreads), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ResponseAddMessageThreads>> AddMessageThreads([FromBody] AddMesaggeThreads data, [FromRoute] string id, CancellationToken cancellationToken = default)
        {
            return await _serviceVirtualAssistan.AddMessageThreads(data, id, cancellationToken);
        }

        [HttpPost("threads/{id}/run")]
        [SwaggerOperation(OperationId = "RunThreads", Tags = new[] { "ApiLia" })]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseRunThreads), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ResponseRunThreads>> RunThreads([FromRoute] string id, CancellationToken cancellationToken = default)
        {
            return await _serviceVirtualAssistan.RunAssistanThreads(id, cancellationToken);
        }

        [HttpPost("threads/{idThread}/run/{idRun}/retrieve")]
        [SwaggerOperation(OperationId = "RetrieveRunThreads", Tags = new[] { "ApiLia" })]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseRunThreads), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ResponseRunThreads>> RetriveRunThreads([FromRoute] string idThread, [FromRoute] string idRun, CancellationToken cancellationToken = default)
        {
            return await _serviceVirtualAssistan.RetriveRunAssistanThreads(idThread, idRun, cancellationToken);
        }

        [HttpGet("threads/{idThread}/messages")]
        [SwaggerOperation(OperationId = "GetMessagesThreads", Tags = new[] { "ApiLia" })]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseGetMessageThreads), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ResponseGetMessageThreads>> GetMessages([FromRoute] string idThread, CancellationToken cancellationToken = default)
        {
            return await _serviceVirtualAssistan.GetMessageThread(idThread, cancellationToken);
        }
    }
}
