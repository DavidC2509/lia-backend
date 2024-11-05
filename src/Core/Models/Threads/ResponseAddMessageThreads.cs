namespace Lia.Core.Models.Threads
{
    public class ResponseAddMessageThreads
    {
        public string Id { get; set; }
        public string Object { get; set; }
        public int Created_at { get; set; }
        public string Thread_id { get; set; }
        public string Role { get; set; }
        public List<ContentData> Content { get; set; }

        public ResponseAddMessageThreads()
        {
            Id = string.Empty;
            Object = string.Empty;
            Thread_id = string.Empty;
            Role = string.Empty;
            Content = new List<ContentData>();
        }

        public class ContentData
        {
            public string Type { get; set; }
            public TextData Text { get; set; }

            public ContentData()
            {
                Type = string.Empty;
                Text = new TextData();
            }
        }

        public class TextData
        {
            public string Value { get; set; }

            public TextData()
            {
                Value = string.Empty;
            }
        }
    }
}
