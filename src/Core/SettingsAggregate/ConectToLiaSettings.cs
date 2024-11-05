namespace Lia.Core.SettingsAggregate
{
    public class ConectToLiaSettings
    {
        public string APIUrl { get; set; }
        public string APIToken { get; set; }
        public string APIAssistan { get; set; }

        public ConectToLiaSettings()
        {
            APIUrl = string.Empty;
            APIToken = string.Empty;
            APIAssistan = string.Empty;
        }
    }

}