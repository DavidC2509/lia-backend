namespace Lia.SharedKernel.Interface
{
    /// <summary>
    /// Interface child front Root
    /// </summary>
    /// <typeparam name="TRoot">EntityRoot</typeparam>
    public interface IAggregateChild<TRoot> where TRoot : IAggregateRoot
    {
    }
}