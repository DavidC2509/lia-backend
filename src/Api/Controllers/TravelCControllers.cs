using Lia.Core.Interface;
using Lia.Core.Models.Threads;
using Lia.Core.Models.TravelC;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Net.Mime;

namespace Lia.Api.Controllers
{
    [ApiController]
    [Route("api/travelC")]
    public class TravelCControllers : ControllerBase
    {
        private readonly ITravelCServices _serviceVirtualAssistan;

        public TravelCControllers(ITravelCServices serviceVirtualAssistan)
        {
            _serviceVirtualAssistan = serviceVirtualAssistan;
        }

        [HttpGet("get-token")]
        [SwaggerOperation(OperationId = "GetTokenUser", Tags = new[] { "ApiTravelC" })]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseCreateThreads), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<string>> GetToken(CancellationToken cancellationToken = default)
        {
            return await _serviceVirtualAssistan.AuthenticateAsync(cancellationToken);
        }

        [HttpGet("get-theme")]
        [SwaggerOperation(OperationId = "GetTheme", Tags = new[] { "ApiTravelC" })]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseCreateThreads), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ResponseThemeTravelC>> GetTheme(CancellationToken cancellationToken = default)
        {
            return await _serviceVirtualAssistan.GetTheme(cancellationToken);
        }

        [HttpGet("get-travel-idea/theme/{idTheme}")]
        [SwaggerOperation(OperationId = "GetTravelIdea", Tags = new[] { "ApiTravelC" })]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseCreateThreads), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ResponseTravelIdea>> GetTravelIdea([FromRoute] int idTheme, CancellationToken cancellationToken = default)
        {
            return await _serviceVirtualAssistan.GetTravellIdea(idTheme, cancellationToken);
        }


    }
}
