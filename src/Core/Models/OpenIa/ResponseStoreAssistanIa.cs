namespace Lia.Core.Models.OpenIa
{
    public class ResponseStoreAssistanIa
    {
        public string Id { get; set; }
        public string Object { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Model { get; set; }
        public string Instructions { get; set; }
        public string Response_format { get; set; }
        public double Top_p { get; set; }
        public double Temperature { get; set; }

        public ResponseStoreAssistanIa()
        {
            Id = string.Empty;
            Object = string.Empty;
            Name = string.Empty;
            Description = string.Empty;
            Model = string.Empty;
            Instructions = string.Empty;
            Response_format = string.Empty;
        }
    }
}
