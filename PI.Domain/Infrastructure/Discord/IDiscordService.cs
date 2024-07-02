using Autofac.Core;
using Discord;

namespace PI.Domain.Infrastructure.Discord
{
    public interface IDiscordService : IDisposable
    {

        //Task StartAsync();

        Task SendErrorMessage(string message);
        Task SendInfoMesssage(string message);
        Task SendWarnMessage(string message);
    }
}
