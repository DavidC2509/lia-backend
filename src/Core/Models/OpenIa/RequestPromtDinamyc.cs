namespace Lia.Core.Models.OpenIa
{
    public class RequestPromtDinamyc
    {
        public string FileName { get; set; }
        public string RoleSystem { get; set; }
        public string ContentSystem { get; set; }
        public List<PromtData> PromData { get; set; }

        public RequestPromtDinamyc()
        {
            FileName = string.Empty;
            RoleSystem = string.Empty;
            ContentSystem = string.Empty;
            PromData = new List<PromtData>();
        }
        public class PromtData
        {
            public string MessageUser { get; set; }
            public string MessageAssistan { get; set; }

            public PromtData()
            {
                MessageAssistan = string.Empty;
                MessageUser = string.Empty;
            }
        }
    }
}
