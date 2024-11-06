namespace Lia.Core.SettingsAggregate
{
    public class ConnectToTravelCSettings
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string MicrositeId { get; set; }
        public string TravelCEndpoint { get; set; }

        public ConnectToTravelCSettings()
        {
            Username = string.Empty;
            Password = string.Empty;
            MicrositeId = string.Empty;
            TravelCEndpoint = string.Empty;
        }

    }
}
