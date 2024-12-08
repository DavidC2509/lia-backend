namespace Lia.Infrastructure.Models
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public int ExpirationInSeconds { get; set; }

        public AuthResponse()
        {
            Token = string.Empty;
        }
    }

}
