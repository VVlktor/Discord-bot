using Microsoft.Extensions.Configuration;

namespace DiscordBot.AiChat
{
    public class ConversationHistory
    {
        public string EndPoint;
        private List<(string, string)> _conv = new();//TODO: lepszą opcją jest by każdy użytkownik miał swoją własną historię czatowania z ai
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

        public List<(string, string)> GetConv()
        {
            return _conv;
        }

        public void AddMessage(string author, string message)
        {
            message = message.Replace("\"", "'");
            _conv.Add((author, message));
        }

        public void DeleteMessages()
        {
            _conv.Clear();
        }
    }
}
