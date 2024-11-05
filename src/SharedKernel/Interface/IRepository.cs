using Ardalis.Specification;

namespace Lia.SharedKernel.Interface
{
    public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
    {
        Task<T> SingleOrDefaultWithErrorMessage(ISingleResultSpecification<T> specification, string errorMessage, CancellationToken cancellationToken = default);
    }
}