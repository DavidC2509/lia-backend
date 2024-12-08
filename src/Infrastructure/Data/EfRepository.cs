using Lia.SharedKernel.Interface;

namespace Lia.Infrastructure.Data
{
    public class EfRepository<T> : RepositoryBaseLocal<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
    {
        public EfRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}