namespace Lia.Infrastructure.Models
{
    public class AuthRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string MicrositeId { get; set; }

        public AuthRequest()
        {
            Username = string.Empty;
            Password = string.Empty;
            MicrositeId = string.Empty;
        }
    }
}
