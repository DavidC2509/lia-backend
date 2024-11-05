using Ardalis.Specification;

namespace Lia.SharedKernel.Interface
{
    public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
    {
        Task<List<T>> ListAsyncAsNoTrackin(CancellationToken cancellationToken = default);
    }
}