namespace Lia.Core.Models.Threads
{
    public class ResponseGetMessageThreads
    {
        public string Object { get; set; }
        public List<ResponseGetMessageThreadsData> Data { get; set; }

        public ResponseGetMessageThreads()
        {
            Object = string.Empty;
            Data = new List<ResponseGetMessageThreadsData>();
        }

        public class ResponseGetMessageThreadsData
        {
            public string Id { get; set; }
            public string Object { get; set; }
            public int Created_at { get; set; }
            public string Role { get; set; }
            public string Thread_id { get; set; }
            public string? Assistant_id { get; set; }
            public List<ResponseGetMessageThreadsDataContent> Content { get; set; }

            public ResponseGetMessageThreadsData()
            {
                Id = string.Empty;
                Object = string.Empty;
                Role = string.Empty;
                Thread_id = string.Empty;
                Content = new List<ResponseGetMessageThreadsDataContent>();
            }

        }

        public class ResponseGetMessageThreadsDataContent
        {
            public string Type { get; set; }
            public ResponseGetMessageThreadsDataContentText Text { get; set; }

            public ResponseGetMessageThreadsDataContent()
            {
                Type = string.Empty;
                Text = new ResponseGetMessageThreadsDataContentText();
            }


        }

        public class ResponseGetMessageThreadsDataContentText
        {
            public string Value { get; set; }
            public ResponseGetMessageThreadsDataContentText()
            {
                Value = string.Empty;
            }
        }
    }
}
