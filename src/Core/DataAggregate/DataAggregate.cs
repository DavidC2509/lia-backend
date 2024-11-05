using Lia.SharedKernel;
using Lia.SharedKernel.Interface;

namespace Lia.Core.DataAggregate
{
    public class DataAggregate : BaseEntity, IAggregateRoot
    {
        //Code Plan de Pago
        public string PaymentPlan { get; set; }

        //Code Responsable
        public string Responsible { get; set; }

        public DataAggregate() : base()
        {
            PaymentPlan = string.Empty;
            Responsible = string.Empty;
        }

    }
}