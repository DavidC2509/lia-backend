namespace Lia.Core.Models.Threads
{
    public class AddMesaggeThreads
    {
        public string Role { get; set; }
        public string Content { get; set; }

        public AddMesaggeThreads()
        {
            Role = string.Empty;
            Content = string.Empty;
        }
    }
}
