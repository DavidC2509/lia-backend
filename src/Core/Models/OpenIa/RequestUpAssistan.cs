namespace Lia.Core.Models.OpenIa
{
    public class RequestUpAssistan
    {
        public string Instructions { get; set; }

        public string Name { get; set; }

        public List<RequestUpAssistanTool> Tools { get; set; }

        public string Model { get; set; } = "gpt-4o-mini";

        public RequestUpAssistan()
        {
            Instructions = string.Empty;
            Name = string.Empty;
            Tools = new List<RequestUpAssistanTool>();
            Model = "gpt-4o-mini";
        }

        public RequestUpAssistan(string name, string instructions, string model, string typeTools) : this()
        {
            Instructions = instructions;
            Name = name;
            Model = model;
            var tools = new RequestUpAssistanTool(typeTools);
            Tools.Add(tools);
        }

        public class RequestUpAssistanTool
        {
            public string Type { get; set; } = "code_interpreter";

            public RequestUpAssistanTool()
            {
                Type = "code_interpreter";
            }

            public RequestUpAssistanTool(string type)
            {
                Type = type;
            }
        }
    }
}
