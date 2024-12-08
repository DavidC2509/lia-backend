namespace Lia.Api.ModelsEntry
{
    public class CreatePromDinamycModel
    {
        public required string NameEvent { get; set; }
        public required DateTime DateEvent { get; set; }
        public required string CityEvent { get; set; }
        public required string AddresEvent { get; set; }
        public required string AdditionalInformation { get; set; }
        public required string NameClient { get; set; }
        public required string PackageTitle { get; set; }
        public required int CountNigthsHotel { get; set; }
        public required string Vigency { get; set; }

    }
}
