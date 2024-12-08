namespace Lia.Core.Models.TravelC
{
    public class ResponseTravelIdea
    {
        public PaginationData Pagination { get; set; }
        public List<IdeaData> Idea { get; set; }

        public ResponseTravelIdea()
        {

            Pagination = new PaginationData();
            Idea = new List<IdeaData>();
        }


        public class PaginationData
        {
            public int FirstResult { get; set; }
            public int PageResults { get; set; }
            public int TotalResults { get; set; }
        }

        public class IdeaData
        {
            public int Id { get; set; }
            public string User { get; set; }
            public string Email { get; set; }
            public string Title { get; set; }
            public string LargeTitle { get; set; }
            public string ImageUrl { get; set; }
            public string CreationDate { get; set; }
            public string DepartureDate { get; set; }
            public string IdeaUrl { get; set; }
            public List<string> Themes { get; set; }
            public Price PricePerPerson { get; set; }
            public Price TotalPrice { get; set; }
            public List<Destination> Destinations { get; set; }
            public bool UserB2c { get; set; }
            public CountersData Counters { get; set; }

            public IdeaData()
            {
                User = string.Empty;
                Email = string.Empty;
                Title = string.Empty;
                LargeTitle = string.Empty;
                ImageUrl = string.Empty;
                CreationDate = string.Empty;
                DepartureDate = string.Empty;
                IdeaUrl = string.Empty;
                Themes = new List<string>();
                PricePerPerson = new Price();
                TotalPrice = new Price();
                Destinations = new List<Destination>();
                Counters = new CountersData();
            }

            public class Price
            {
                public double Amount { get; set; }
                public string Currency { get; set; }

                public Price()
                {
                    Currency = string.Empty;
                }
            }
            public class CountersData
            {
                public int Adults { get; set; }
                public int Children { get; set; }
                public int Destinations { get; set; }
                public int ClosedTours { get; set; }
                public int HotelNights { get; set; }
                public int Transports { get; set; }
                public int Hotels { get; set; }
                public int Cars { get; set; }
                public int Tickets { get; set; }
                public int Transfers { get; set; }
                public int Insurances { get; set; }
                public int Manuals { get; set; }
                public int Cruises { get; set; }
                public int RideHailings { get; set; }
            }

            public class Destination
            {
                public string Code { get; set; }
                public string Name { get; set; }

                public Destination()
                {
                    Code = string.Empty;
                    Name = string.Empty;
                }
            }
        }
    }
}
