namespace Lia.Core.Models.TravelC
{
    public class ResponseThemeTravelC
    {
        public List<ResponseThemeTravelCTheme> Theme { get; set; }
        public ResponseThemeTravelC()
        {
            Theme = new List<ResponseThemeTravelCTheme>();
        }
    }

    public class ResponseThemeTravelCTheme
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public ResponseThemeTravelCTheme()
        {
            Name = string.Empty;
            ImageUrl = string.Empty;
        }
    }
}
