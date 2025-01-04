using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DiscordBot.DBot
{
    public class Bot : IBot
    {
        private ServiceProvider _serviceProvider;
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
            _client = new DiscordSocketClient();
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
        }

        public async Task HandleCommandAsync(SocketMessage mes)
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
