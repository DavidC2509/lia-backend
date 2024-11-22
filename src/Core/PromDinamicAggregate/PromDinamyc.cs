using Lia.SharedKernel;
using Lia.SharedKernel.Interface;

namespace Lia.Core.PromDinamicAggregate
{
    public class PromDinamyc : BaseEntity, IAggregateRoot
    {
        //Code Plan de Pago
        public string NameEvent { get; set; }
        public DateTime DateEvent { get; set; }
        public string CityEvent { get; set; }
        public string AddresEvent { get; set; }
        public string AdditionalInformation { get; set; }
        public string? PromModified { get; set; }

        public PromDinamyc()
        {
            NameEvent = string.Empty;
            CityEvent = string.Empty;
            AdditionalInformation = string.Empty;
            AddresEvent = string.Empty;
        }

        public PromDinamyc(string nameEvent, DateTime dateEvent, string cityEvent, string addresEvent, string additionalInformation)
        {
            NameEvent = nameEvent;
            DateEvent = dateEvent;
            CityEvent = cityEvent;
            AddresEvent = addresEvent;
            AdditionalInformation = additionalInformation;
        }
    }
}
