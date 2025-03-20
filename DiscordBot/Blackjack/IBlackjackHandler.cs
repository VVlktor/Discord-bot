using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Blackjack
{
    public interface IBlackJackHandler
    {
        public Task HandleStart(SocketMessageComponent component);
        public Task HandleHit(SocketMessageComponent component);
        public Task HandleStand(SocketMessageComponent component);
    }
}
