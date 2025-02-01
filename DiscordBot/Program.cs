using DiscordBot.AiChat;
using DiscordBot.DBot;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DiscordBot
{
    internal class Program
    {
        private static void Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();

        private static async Task MainAsync(string[] args)
        {
            var configuration = new ConfigurationBuilder().AddUserSecrets(Assembly.GetExecutingAssembly()).Build();

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConfiguration>(configuration)
                .AddSingleton<ConversationHistory>()
                .AddScoped<IBot, Bot>()
                .AddScoped<IJsonGeneratorForAi, JsonGeneratorForAi>()
                .AddHttpClient<AiService>(c => {
                    c.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("appliaction/json"));
                    c.DefaultRequestHeaders.Add("User-Agent", "DiscordBot");
                }).Services
                .BuildServiceProvider();

            IBot bot = serviceProvider.GetRequiredService<IBot>();

            await bot.StartAsync(serviceProvider);

            do
            {
                var keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.S)
                {
                    await bot.StopAsync();
                    return;
                }
            } while (true);
        }
    }
}
