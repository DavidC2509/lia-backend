using Lia.SharedKernel;
using Lia.SharedKernel.Interface;

namespace Lia.Core.PromAggregate
{
    public class Prom : BaseEntity, IAggregateRoot
    {
        //Code Plan de Pago
        public string PromModified { get; set; }

        public Prom()
        {
            PromModified = string.Empty;
        }

        public Prom(string promModified)
        {
            PromModified = promModified;
            Id = Guid.NewGuid();
        }
    }
}
