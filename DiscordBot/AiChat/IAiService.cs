namespace DiscordBot.AiChat
{
    public interface IAiService
    {
        Task<string> GetAiResponse(string userInput, string username);
        void ResetChat(string username);
    }
}
