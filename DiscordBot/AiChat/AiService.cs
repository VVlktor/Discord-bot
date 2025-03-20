using System.Text;

namespace DiscordBot.AiChat
{
    public class AiService : IAiService
    {
        private ConversationHistory _conversationHistory;
        private HttpClient _httpClient;
        private IJsonGeneratorForAi _jsonGeneratorForAi;
        private IAiResponseReader _aiResponseReader;

        public AiService(IJsonGeneratorForAi jsonGeneratorForAi, IAiResponseReader aiResponseReader, HttpClient httpClient, ConversationHistory conversationHistory)
        {
            _aiResponseReader = aiResponseReader;
            _httpClient = httpClient;
            _jsonGeneratorForAi = jsonGeneratorForAi;
            _conversationHistory = conversationHistory;
        }

        public async Task<string> GetAiResponse(string userInput, string username)
        {
            _conversationHistory.AddMessage(username, "user", userInput);
            string json = _jsonGeneratorForAi.GenerateJson(_conversationHistory.GetConv(username));
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage responseMessage = await _httpClient.PostAsync(_conversationHistory.EndPoint, content);
                if (!responseMessage.IsSuccessStatusCode)
                    return "Problem occurred";

                string responseContent = await responseMessage.Content.ReadAsStringAsync();
                string messageForUser = _aiResponseReader.ExtractAIResponse(responseContent);
                _conversationHistory.AddMessage(username, "model", messageForUser);
                return messageForUser;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "Problem occurred";
        }

        public void ResetChat(string username)
        {
            _conversationHistory.DeleteMessages(username);
        }
    }
}
