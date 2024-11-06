using Lia.Core.SettingsAggregate;
using Lia.Infrastructure.Service;
using Lia.SharedKernel.Interface;

namespace Lia.Api.Extensions
{
    public static class TransactionInjectionExtension
    {
        public static IServiceCollection AddTransactionDependencies(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAutoMapper(typeof(Program));
            services.AddScoped<IErrorPropertiesFactory, ErrorPropertiesFactory>();

            services.AddCors(o => o.AddPolicy("CorePolicy", builder =>
            {
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
                builder.AllowAnyOrigin();

            }));
            // Configurar HttpClient con un nombre específico sin BaseAddress
            services.AddHttpClient();
            services.Configure<ConectToLiaSettings>(configuration.GetSection("ApiLia"));
            services.Configure<ConnectToTravelCSettings>(configuration.GetSection("ApiTravelCSettings"));
            return services;
        }
    }
}