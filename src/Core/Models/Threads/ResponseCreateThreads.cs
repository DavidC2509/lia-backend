namespace Lia.Core.Models.Threads
{
    public class ResponseCreateThreads
    {
        public string Id { get; set; }
        public string Object { get; set; }
        public int Created_at { get; set; }

        public ResponseCreateThreads()
        {
            Id = string.Empty;
            Object = string.Empty;
        }
    }
}
