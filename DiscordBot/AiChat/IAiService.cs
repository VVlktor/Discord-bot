namespace DiscordBot.AiChat
{
    public interface IAiService
    {
        Task<string> GetAiResponse(string userInput);
        void ResetChat();
    }
}
