using Lia.Core.Interface;
using Lia.Core.Models.OpenIa;
using Lia.Core.Models.Threads;
using Lia.Core.SettingsAggregate;
using Lia.SharedKernel.Utils;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Lia.Infrastructure.Interface
{
    public class VirtualAssistant : IVirtualAssistant
    {
        private readonly HttpClient _httpClient;

        private readonly ConectToLiaSettings _apiSettings;

        public VirtualAssistant(IOptions<ConectToLiaSettings> apiSettings)
        {
            _httpClient = new HttpClient();
            _apiSettings = apiSettings.Value;
        }


        public async Task<ResponseCreateThreads> CreateThreads(CancellationToken cancellationToken)
        {
            AddHeader();
            var request = DataUtils.CreateHtpRequest(HttpMethod.Post, _apiSettings.APIUrl + "threads");
            return await _httpClient.SendConvertData<ResponseCreateThreads>(request, cancellationToken);
        }

        public async Task<ResponseAddMessageThreads> AddMessageThreads(AddMesaggeThreads data, string threadId, CancellationToken cancellationToken)
        {
            AddHeader();
            var request = DataUtils.CreateHtpRequest(HttpMethod.Post, data, _apiSettings.APIUrl + "threads/" + threadId + "/messages");
            return await _httpClient.SendConvertData<ResponseAddMessageThreads>(request, cancellationToken);
        }

        public async Task<ResponseRunThreads> RunAssistanThreads(string threadId, CancellationToken cancellationToken)
        {
            var body = new { assistant_id = _apiSettings.APIAssistan };
            AddHeader();
            var request = DataUtils.CreateHtpRequest(HttpMethod.Post, body, _apiSettings.APIUrl + "threads/" + threadId + "/runs");
            return await _httpClient.SendConvertData<ResponseRunThreads>(request, cancellationToken);
        }

        public async Task<ResponseRunThreads> RetriveRunAssistanThreads(string threadId, string runsId, CancellationToken cancellationToken)
        {
            AddHeader();
            var request = DataUtils.CreateHtpRequest(HttpMethod.Get, _apiSettings.APIUrl + "threads/" + threadId + "/runs/" + runsId);
            return await _httpClient.SendConvertData<ResponseRunThreads>(request, cancellationToken);
        }

        public async Task<ResponseGetMessageThreads> GetMessageThread(string threadId, CancellationToken cancellationToken)
        {
            AddHeader();
            var request = DataUtils.CreateHtpRequest(HttpMethod.Get, _apiSettings.APIUrl + "threads/" + threadId + "/messages");
            return await _httpClient.SendConvertData<ResponseGetMessageThreads>(request, cancellationToken);
        }

        public async Task<ListFilesOpenIA> GetListFiles(CancellationToken cancellationToken)
        {
            AddHeader();
            var request = DataUtils.CreateHtpRequest(HttpMethod.Get, _apiSettings.APIUrl + "files");
            return await _httpClient.SendConvertData<ListFilesOpenIA>(request, cancellationToken);
        }

        public async Task<ResponeAddFileOpenIa> AddFileContent(StreamContent file, string fileName, CancellationToken cancellationToken)
        {
            AddHeader();
            var purpose = "fine-tune";
            var request = DataUtils.CreateHtpRequest(HttpMethod.Post, _apiSettings.APIUrl + "files");
            // Usar AddFormData para agregar el contenido
            request.AddFormData(
                new Dictionary<string, string>
                {
            { "purpose", purpose }
                },
                file,
                fileName
            );
            return await _httpClient.SendConvertData<ResponeAddFileOpenIa>(request, cancellationToken);
        }

        public async Task<ResponeAddFileOpenIa> DynamycPromt(List<MessagesOutput> data, string filename, CancellationToken cancellationToken)
        {
            var content = SaveAsJsonlToStream(data);
            AddHeader();
            var purpose = "fine-tune";
            var request = DataUtils.CreateHtpRequest(HttpMethod.Post, _apiSettings.APIUrl + "files");
            // Usar AddFormData para agregar el contenido
            request.AddFormData(
                new Dictionary<string, string>
                {
            { "purpose", purpose }
                },
                content,
                filename
            );
            return await _httpClient.SendConvertData<ResponeAddFileOpenIa>(request, cancellationToken);
        }

        public async Task<ResponseStoreAssistanIa> UpAssistan(RequestUpAssistan data, CancellationToken cancellationToken)
        {
            AddHeader();
            var request = DataUtils.CreateHtpRequest(HttpMethod.Post, data, _apiSettings.APIUrl + "assistants");
            return await _httpClient.SendConvertData<ResponseStoreAssistanIa>(request, cancellationToken);
        }

        public static StreamContent SaveAsJsonlToStream(List<MessagesOutput> messagesOutput)
        {
            // Crear un MemoryStream para almacenar el contenido
            var memoryStream = new MemoryStream();

            using (var writer = new StreamWriter(memoryStream, new System.Text.UTF8Encoding(false), leaveOpen: true))
            {
                // Configuración para evitar el escapado innecesario
                var jsonSerializerOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, // Permitir caracteres sin escapado
                    WriteIndented = false // JSONL no requiere formato indentado
                };

                foreach (var output in messagesOutput)
                {
                    var jsonLine = JsonSerializer.Serialize(output, jsonSerializerOptions);
                    writer.WriteLine(jsonLine);
                }
            }

            // Reiniciar la posición del MemoryStream para lectura
            memoryStream.Position = 0;

            // Crear y devolver StreamContent
            return new StreamContent(memoryStream);
        }



        public void AddHeader()
        {
            _httpClient.AddTokenTsoPromise(_apiSettings.APIToken);
            _httpClient.AddCustomHeader("OpenAI-Beta", "assistants=v2");
        }


    }
}
