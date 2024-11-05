using Microsoft.EntityFrameworkCore.Storage;
using Npgsql;

namespace Lia.Infrastructure
{
    public class RetryingPostgresExecutionStrategy : ExecutionStrategy
    {
        public RetryingPostgresExecutionStrategy(ExecutionStrategyDependencies dependencies)
            : base(dependencies, DefaultMaxRetryCount, DefaultMaxDelay)
        {
        }

        protected override bool ShouldRetryOn(Exception exception)
        {
            if (exception is PostgresException postgresException)
            {
                if (postgresException.IsTransient)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return ShouldRetryOn(exception);
        }
    }
}
