using Microsoft.Extensions.Configuration;

namespace DiscordBot.AiChat
{
    public class ConversationHistory
    {
        public string EndPoint;
        private List<UserConversastion> _conv = new();
        private IConfiguration _configuration;

        public ConversationHistory(IConfiguration config)
        {
            _configuration = config;
            SetEndPoint();
        }

        private void SetEndPoint()
        {
            string apiKey = _configuration["AiToken"] ?? throw new Exception("Brak tokenu ai");
            string endpoint = "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash-latest:generateContent";
            EndPoint = $"{endpoint}?key={apiKey}";
        }

        public List<(string, string)> GetConv(string username)
        {
            var userConversation = _conv.FirstOrDefault(x => x.Username == username);
            return userConversation?.Conversation ?? new List<(string, string)>();
        }

        public void AddMessage(string username, string author, string message)
        {
            message = message.Replace("\"", "'");
            var userConversation = _conv.FirstOrDefault(x => x.Username == author);
            if (userConversation is null)
            {
                userConversation = new UserConversastion(author);
                _conv.Add(userConversation);
            }
            userConversation.Conversation.Add((author, message));
        }

        public void DeleteMessages(string username)
        {
            _conv.RemoveAll(x => x.Username == username);
        }
    }
}
