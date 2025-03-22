using Discord;
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
        private Dictionary<string, (int player, int bot)> gameValues;
        private Random _random;

        public BlackjackHandler()
        {
            gameValues = new();
            _random = new Random();
        }

        private Embed BuildFirstMessage(string username)
        {
            var x = gameValues.Where(x => x.Key == username).First();
            var mes = new EmbedBuilder().WithTitle($"Dealer: {x.Value.bot}\nPlayer: {x.Value.player}").WithDescription("Hit or stand?");
            return mes.Build();
        }

        private void BeginSystemGame(string username)
        {
            gameValues.Remove(username);
            gameValues.Add(username, (_random.Next(3, 18), _random.Next(15, 20)));
        }

        public async Task HandleStart(SocketMessageComponent component)
        {
            await component.DeferAsync();
            BeginSystemGame(component.User.Username);
            var embed = BuildFirstMessage(component.User.Username);
            MessageComponent comps = new ComponentBuilder().WithButton(label: "Hit", customId: "bj-hit").WithButton(label: "Stand", customId: "bj-stand").Build();
            await component.Message.ModifyAsync(msg =>
            {
                msg.Embed = embed;
                msg.Content = $"Player {component.User.Mention} is playing right now!";
                msg.Components = comps;
            });
        }

        public async Task HandleHit(SocketMessageComponent component)
        {
            string username = component.User.Username;

            if (!component.Message.Content.Contains(component.User.Mention))
                return;

            var values = gameValues[$"{username}"];
            int hisNewCard = _random.Next(1, 9);
            gameValues[$"{username}"] = (values.player + hisNewCard, values.bot);
            values = gameValues[$"{username}"];

            var embed = new EmbedBuilder().WithTitle($"Dealer: {values.bot}\nPlayer: {values.player}").WithDescription($"You draw {hisNewCard}!").Build();
            MessageComponent buttons = new ComponentBuilder().WithButton(label: "Start new game", customId: "bj-start").Build();
            string content = $"Player {component.User.Mention} is playing!";

            if (values.player > 21)
                content = $"Player {component.User.Mention} lost!";
            else if (values.player == 21)
                content = $"Player {component.User.Mention} wins!";

            await component.Message.ModifyAsync(msg =>
            {
                msg.Embed = embed;
                msg.Content = content;
                if (values.player >= 21)
                    msg.Components = buttons;
            });
            await component.DeferAsync();
        }

        public async Task HandleStand(SocketMessageComponent component)
        {
            if (!component.Message.Content.Contains(component.User.Mention))
                return;

            string username = component.User.Username;
            int botDiff = 21 - gameValues[$"{username}"].bot;
            int playerDiff = 21 - gameValues[$"{username}"].player;

            string message;
            if (playerDiff > botDiff)
                message = $"Player {component.User.Mention} lost!";
            else if (playerDiff == botDiff)
                message = $"Player {component.User.Mention} ended match with a draw!";
            else
                message = $"Player {component.User.Mention} wins!";

            MessageComponent buttons = new ComponentBuilder().WithButton(label: "Start new game", customId: "bj-start").Build();
            await component.Message.ModifyAsync(msg =>
            {
                msg.Components = buttons;
                msg.Content = message;
            });
            await component.DeferAsync();
        }
    }
}
