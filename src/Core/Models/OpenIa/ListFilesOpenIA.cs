namespace Lia.Core.Models.OpenIa
{
    public class ListFilesOpenIA
    {
        public List<ListFilesOpenIAData> Data { get; set; }

        public ListFilesOpenIA()
        {
            Data = new List<ListFilesOpenIAData>();
        }

        public class ListFilesOpenIAData
        {
            public string Object { get; set; }

            public string Id { get; set; }

            public string Purpose { get; set; }

            public string Filename { get; set; }
            public int Bytes { get; set; }

            public string Status { get; set; }

            public ListFilesOpenIAData()
            {
                Object = string.Empty;
                Id = string.Empty;
                Purpose = string.Empty;
                Filename = string.Empty;
                Status = string.Empty;
            }
        }
    }
}
