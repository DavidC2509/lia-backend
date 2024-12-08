namespace Lia.Core.Models.Threads
{
    public class ResponseRunThreads
    {
        public string Id { get; set; }
        public string Object { get; set; }
        public int Created_at { get; set; }
        public string Assistant_id { get; set; }
        public string Thread_id { get; set; }
        public string Status { get; set; }
        public int? Started_at { get; set; }
        public int? Expires_at { get; set; }
        public string Model { get; set; }

        public string Instructions { get; set; }

        public ResponseRunThreads()
        {
            Id = string.Empty;
            Object = string.Empty;
            Assistant_id = string.Empty;
            Thread_id = string.Empty;
            Status = string.Empty;
            Instructions = string.Empty;
            Model = string.Empty;
        }
    }
}
