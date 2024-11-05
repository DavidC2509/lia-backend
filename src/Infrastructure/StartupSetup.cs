using Lia.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Lia.Infrastructure
{
    public static class StartupSetup
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString)
        {

            // Add services to the container.
            services.AddDbContext<AppDbContext>(options =>
              options.UseNpgsql(
                connectionString,
                    psqlOptions =>
                    {
                        psqlOptions.MigrationsAssembly("Lia.Infrastructure").EnableRetryOnFailure();
                        psqlOptions.ExecutionStrategy(c => new RetryingPostgresExecutionStrategy(c));
                    }));
        }

    }
}
