using Lia.Core.Interface;
using Lia.Core.Models.OpenIa;
using Lia.Core.Models.Threads;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Net.Mime;

namespace Lia.Api.Controllers
{
    [ApiController]
    [Route("api/open-ia")]
    public class OpenIaController : ControllerBase
    {
        private readonly IVirtualAssistant _serviceVirtualAssistan;

        public OpenIaController(IVirtualAssistant serviceVirtualAssistan)
        {
            _serviceVirtualAssistan = serviceVirtualAssistan;
        }


        [HttpPost("store-file")]
        [SwaggerOperation(OperationId = "StoreFile", Tags = new[] { "ApiIa" })]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponeAddFileOpenIa), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ResponeAddFileOpenIa>> CreateFile(IFormFile file, CancellationToken cancellationToken = default)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No se ha proporcionado un archivo o está vacío.");
            }

            // Validar que el archivo tenga la extensión .jsonl
            if (!file.FileName.EndsWith(".jsonl", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("El archivo debe tener la extensión .jsonl.");
            }

            // Agrega el archivo al contenido del formulario
            using var fileStream = file.OpenReadStream();
            var fileContent = new StreamContent(fileStream);

            return await _serviceVirtualAssistan.AddFileContent(fileContent, file.FileName, cancellationToken);
        }

        [HttpGet("get-list-file")]
        [SwaggerOperation(OperationId = "GetFiles", Tags = new[] { "ApiIa" })]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseCreateThreads), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ListFilesOpenIA>> GetListFiles(CancellationToken cancellationToken = default)
        {
            return await _serviceVirtualAssistan.GetListFiles(cancellationToken);
        }

        [HttpPost("store-promt-dinamyc")]
        [SwaggerOperation(OperationId = "StorePromtDinamyc", Tags = new[] { "ApiIa" })]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponeAddFileOpenIa), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ResponeAddFileOpenIa>> CreatePromtDinamyc(RequestPromtDinamyc data, CancellationToken cancellationToken = default)
        {
            var messagesOutput = ConvertToMessagesOutput(data);
            return await _serviceVirtualAssistan.DynamycPromt(messagesOutput, data.FileName, cancellationToken);
        }

        public static List<MessagesOutput> ConvertToMessagesOutput(RequestPromtDinamyc input)
        {
            var result = new List<MessagesOutput>();

            foreach (var promData in input.PromData)
            {
                var messages = new MessagesOutput();

                // Agrega el mensaje del sistema (role: system)
                messages.Messages.Add(new MessagesOutput.Message("system", input.ContentSystem));

                // Agrega el mensaje del usuario (role: user)
                messages.Messages.Add(new MessagesOutput.Message("user", promData.MessageUser));

                // Agrega el mensaje del asistente (role: assistant)
                messages.Messages.Add(new MessagesOutput.Message("assistant", promData.MessageAssistan));

                result.Add(messages);
            }

            return result;
        }

    }
}
