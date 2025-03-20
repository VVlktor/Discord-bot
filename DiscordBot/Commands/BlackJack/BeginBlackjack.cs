using Discord.Commands;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Commands.BlackJack
{
    public class BeginBlackjack : ModuleBase<SocketCommandContext>
    {
        [Command("blackjack")]
        public async Task ExecuteAsync()
        {
            var button = new ComponentBuilder().WithButton(label: "Begin", customId: "bj-start");
            await ReplyAsync("Begin blackjack game!", components: button.Build());
        }
    }
}
