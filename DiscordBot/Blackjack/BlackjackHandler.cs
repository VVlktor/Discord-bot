using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Blackjack
{
    public class BlackjackHandler : IBlackJackHandler
    {
        public Task HandleHit(SocketMessageComponent component)
        {
            throw new NotImplementedException();
        }

        public Task HandleStand(SocketMessageComponent component)
        {
            throw new NotImplementedException();
        }

        public Task HandleStart(SocketMessageComponent component)
        {
            throw new NotImplementedException();
        }
    }
}
