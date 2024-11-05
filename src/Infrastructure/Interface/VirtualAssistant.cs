using Lia.Core.Interface;
using Lia.Core.Models.Threads;
using Lia.Core.SettingsAggregate;
using Lia.SharedKernel.Utils;
using Microsoft.Extensions.Options;

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

        public void AddHeader()
        {
            _httpClient.AddTokenTsoPromise(_apiSettings.APIToken);
            _httpClient.AddCustomHeader("OpenAI-Beta", "assistants=v2");
        }


    }
}
