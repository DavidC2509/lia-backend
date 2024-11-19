namespace Lia.Core.Models.OpenIa
{
    public class MessagesOutput
    {
        public List<Message> Messages { get; set; }

        public MessagesOutput()
        {
            Messages = new List<Message>();
        }

        public class Message
        {
            public string Role { get; set; }
            public string Content { get; set; }

            public Message(string role, string content)
            {
                Role = role;
                Content = content;
            }
        }
    }

}