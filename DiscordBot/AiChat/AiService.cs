namespace DiscordBot.AiChat
{
    public class AiService : IAiService
    {
        private ConversationHistory _conversationHistory;

        public AiService(ConversationHistory conversationHistory)
        {
            _conversationHistory = conversationHistory;
        }

        public Task<string> GetAiResponse(string userInput)
        {
            throw new NotImplementedException();
        }

        public void ResetChat()
        {
            _conversationHistory.DeleteMessages();
        }
    }
}
