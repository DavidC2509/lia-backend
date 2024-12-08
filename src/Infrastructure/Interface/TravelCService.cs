using Lia.Core.Interface;
using Lia.Core.Models.TravelC;
using Lia.Core.SettingsAggregate;
using Lia.Infrastructure.Models;
using Lia.SharedKernel.Utils;
using Microsoft.Extensions.Options;

namespace Lia.Infrastructure.Interface
{
    public class TravelCService : ITravelCServices
    {
        private readonly HttpClient _httpClient;
        private readonly ConnectToTravelCSettings _apiSettings;

        public TravelCService(HttpClient httpClient, IOptions<ConnectToTravelCSettings> apiSettings)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings.Value;
        }

        public async Task<string> AuthenticateAsync(CancellationToken cancellationToken)
        {
            try
            {
                var authRequest = new AuthRequest()
                {
                    Username = _apiSettings.Username,
                    Password = _apiSettings.Password,
                    MicrositeId = _apiSettings.MicrositeId,
                };

                var request = DataUtils.CreateHtpRequest(HttpMethod.Post, authRequest, _apiSettings.TravelCEndpoint + "authentication/authenticate");
                var token = await _httpClient.SendConvertData<AuthResponse>(request, cancellationToken);
                return token.Token;
            }
            catch (Exception ex)
            {
                throw new Exception("Error authenticating with TravelC", ex);
            }
        }


        public async Task<ResponseThemeTravelC> GetTheme(CancellationToken cancellationToken)
        {
            try
            {
                var token = await AuthenticateAsync(cancellationToken);
                _httpClient.AddCustomHeader("auth-token", token);
                var request = DataUtils.CreateHtpRequest(HttpMethod.Get, _apiSettings.TravelCEndpoint + "/theme/" + _apiSettings.MicrositeId);
                var response = await _httpClient.SendConvertData<ResponseThemeTravelC>(request, cancellationToken);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Error authenticating with TravelC", ex);
            }
        }

        public async Task<ResponseTravelIdea> GetTravellIdea(int themes, CancellationToken cancellationToken)
        {
            string queryString = $"?ids={string.Join(",", themes)}";
            string fullUrl = _apiSettings.TravelCEndpoint + "travelidea/" + _apiSettings.MicrositeId + queryString;
            var token = await AuthenticateAsync(cancellationToken);
            _httpClient.AddCustomHeader("auth-token", token);
            var request = DataUtils.CreateHtpRequest(HttpMethod.Get, fullUrl);
            var response = await _httpClient.SendConvertData<ResponseTravelIdea>(request, cancellationToken);
            return response;
        }
    }
}
