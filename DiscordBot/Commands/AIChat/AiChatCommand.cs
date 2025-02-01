using Discord.Commands;
using DiscordBot.AiChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Commands.AIChat
{
    public class AiChatCommand : ModuleBase<SocketCommandContext>
    {
        private AiService _aiService;

        public AiChatCommand(AiService aiService)
        {
            _aiService = aiService;
        }

        [Command("talk")]
        [Summary("Talk with ai")]
        public async Task Talk([Remainder][Summary("A phrase")] string phrase)
        {
            if (string.IsNullOrEmpty(phrase))
            {
                await ReplyAsync("Usage: !talk <thing you want to talk about wiht ai>");
                return;
            }
            string response = await _aiService.GetAiResponse(phrase, Context.User.Username);
            await ReplyAsync($"{response}");
        }

        [Command("reset")]
        [Summary("Clear the converstion history - ai will forget everything")]
        public async Task Reset()
        {
            _aiService.ResetChat(Context.User.Username);
            await ReplyAsync("Your chat history has been reset.");
        }
    }
}
