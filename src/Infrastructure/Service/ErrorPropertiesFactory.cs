using Lia.SharedKernel.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Lia.Infrastructure.Service
{
    public class ErrorPropertiesFactory : IErrorPropertiesFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger _logger;

        public ErrorPropertiesFactory(IHttpContextAccessor httpContextAccessor, ILogger<ErrorPropertiesFactory> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public IDictionary<string, object?> CreateCommonProperties(Dictionary<string, List<string>>? error)
        {
            var requestFeature = _httpContextAccessor.HttpContext?
                .Features.Get<IHttpRequestFeature>();

            _logger.LogError(message: $"-Fecha y hora: {{{DateTime.Now:dd/MM/yyyy HH:mm:ss.FFFFFF}}}\n      -Petición: [{requestFeature!.Method}] {requestFeature.RawTarget}\n");

            var traceId = Activity.Current?.Id ?? _httpContextAccessor.HttpContext?.TraceIdentifier;

            return new Dictionary<string, object?>
            {
                { "errors", error},
                { "endpoint", requestFeature.RawTarget },
                { "traceId", traceId }
            };
        }

        public IDictionary<string, object?> CreateCommonProperties(string error)
        {
            var requestFeature = _httpContextAccessor.HttpContext?
                .Features.Get<IHttpRequestFeature>();

            _logger.LogError(message: $"-Fecha y hora: {{{DateTime.Now:dd/MM/yyyy HH:mm:ss.FFFFFF}}}\n      -Petición: [{requestFeature!.Method}] {requestFeature.RawTarget}\n");

            var traceId = Activity.Current?.Id ?? _httpContextAccessor.HttpContext?.TraceIdentifier;

            var errorInternal = new Dictionary<string, List<string>>
            {
                { "internal", new List<string> { error } }
            };
            return new Dictionary<string, object?>
            {
                { "errors", errorInternal},
                { "endpoint", requestFeature.RawTarget },
                { "traceId", traceId }
            };
        }
    }
}