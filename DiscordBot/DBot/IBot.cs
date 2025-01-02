using Microsoft.Extensions.DependencyInjection;

namespace DiscordBot.DBot
{
    public interface IBot
    {
        Task StartAsync(ServiceProvider services);

        Task StopAsync();
    }
}
