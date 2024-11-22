namespace Lia.Api.ModelsEntry
{
    public class CreatePromDinamycModel
    {
        public required string NameEvent { get; set; }
        public required DateTime DateEvent { get; set; }
        public required string CityEvent { get; set; }
        public required string AddresEvent { get; set; }
        public required string AdditionalInformation { get; set; }
    }
}
