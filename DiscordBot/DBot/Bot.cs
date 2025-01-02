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
        }

        public Task StopAsync()
        {
            throw new NotImplementedException();
        }
    }
}
