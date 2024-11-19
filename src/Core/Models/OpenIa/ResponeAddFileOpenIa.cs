namespace Lia.Core.Models.OpenIa
{
    public class ResponeAddFileOpenIa
    {
        public string Id { get; set; }
        public string Object { get; set; }
        public int Bytes { get; set; }
        public string Purpose { get; set; }
        public string Filename { get; set; }

        public ResponeAddFileOpenIa()
        {
            Id = string.Empty;
            Object = string.Empty;
            Purpose = string.Empty;
            Filename = string.Empty;
        }
    }
}
