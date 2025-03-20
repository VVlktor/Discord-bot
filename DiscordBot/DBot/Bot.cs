using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Blackjack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DiscordBot.DBot
{
    public class Bot : IBot
    {
        private ServiceProvider? _serviceProvider;
        private readonly IConfiguration _configuration;
        private readonly CommandService _commands;
        private readonly DiscordSocketClient _client;

        public Bot(IConfiguration configuration)
        {
            _configuration = configuration;
            DiscordSocketConfig config = new DiscordSocketConfig()
            {
                GatewayIntents = Discord.GatewayIntents.AllUnprivileged | Discord.GatewayIntents.MessageContent
            };
            _client = new DiscordSocketClient(config);
            _commands = new CommandService();
        }

        public async Task StartAsync(ServiceProvider services)
        {
            string discordToken = _configuration["DiscordToken"] ?? throw new Exception("Brak tokenu");

            _serviceProvider = services;

            await _commands.AddModulesAsync(Assembly.GetExecutingAssembly(), _serviceProvider);

            await _client.LoginAsync(Discord.TokenType.Bot, discordToken);
            await _client.StartAsync();

            _client.MessageReceived += HandleCommandAsync;

            _client.ButtonExecuted += HandleButtonClickAsync;
        }

        private async Task HandleButtonClickAsync(SocketMessageComponent component)
        {
            switch (component.Data.CustomId)
            {
                case "bj-start":
                    //start bj
                    break;
                case "bj-hit":
                     //handle hit
                    break;
                case "bj-stand":
                    //handle stand
                    break;
                default:
                    await component.RespondAsync($"{component.User.Mention} has clicked the button without id lol - yes this is glitch");
                    break;
            }
        }

        private async Task HandleCommandAsync(SocketMessage mes)
        {
            if(mes is not SocketUserMessage message || message.Author.IsBot)
                return;

            int position = 0;
            bool masageIsCommand = message.HasCharPrefix('!', ref position);

            if (masageIsCommand)
            {
                await _commands.ExecuteAsync(new SocketCommandContext(_client, message), position, _serviceProvider);
                return;
            }
        }

        public async Task StopAsync()
        {
            if (_client != null)
            {
                await _client.LogoutAsync();
                await _client.StopAsync();
            }
        }
    }
}
