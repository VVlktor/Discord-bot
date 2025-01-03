using Discord.Commands;

namespace DiscordBot.Commands.Help
{
    public class HelpCommand : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        [Summary("Lists every available command")]
        public async Task ExecuteAsync()
        {
            await ReplyAsync("## Commands\n - **help**: Show every available command");
        }
    }
}
