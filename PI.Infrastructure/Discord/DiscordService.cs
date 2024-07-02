using Discord;
using Discord.Rest;
using Discord.WebSocket;
using PI.Domain.Common;
using PI.Domain.Extensions;
using PI.Domain.Infrastructure.Discord;

namespace PI.Infrastructure.Discord
{
    public class DiscordService : IDiscordService
    {
        //public readonly DiscordSocketClient _client;
        public readonly DiscordRestClient _client;
        private string _token;
        private string _env = string.Empty;
        private string _channelID = string.Empty;

        public DiscordService()
        {
            _client = new DiscordRestClient();
            _token = AppConfig.DiscordConfig.BotToken;

            if (AppConfig.IsDevelopmentEnvironment)
            {
                _env = "Development";
                _channelID = AppConfig.DiscordConfig.NotificationChannelID;
            }
            else
            {
                _env = "Production";
                _channelID = AppConfig.DiscordConfig.NotificationProductionChannelID;
            }

            StartAsync().Wait();
        }

        public async Task StartAsync()
        {
            await _client.LoginAsync(TokenType.Bot, _token);
            //await _client.StartAsync();
            await StartPing();
        }

        //public async Task StopAsync()
        //{
        //    await EndPing();
        //    await _client.StopAsync();
        //}

        private async Task SendMessage(Embed embed, string? message = null)
        {
            var ch = await _client.GetChannelAsync(ulong.Parse(_channelID));
            if (ch != null && ch is IMessageChannel mCh)
            {
                await mCh.SendMessageAsync(text: message, embed: embed);
            }
        }

        public async Task SendErrorMessage(string message)
        {
            var embed = new EmbedBuilder()
            .WithTitle($"{_env} - System Error")
            .WithDescription(message)
            .WithColor(Color.Red)
            .WithCurrentTimestamp()
            .Build();

            await SendMessage(embed);
        }

        public async Task SendInfoMesssage(string message)
        {
            var embed = new EmbedBuilder()
            .WithTitle($"{_env} - System Info")
            .WithDescription(message)
            .WithColor(Color.Blue)
            .WithCurrentTimestamp()
            .Build();

            await SendMessage(embed);
        }

        public async Task SendWarnMessage(string message)
        {
            var embed = new EmbedBuilder()
           .WithTitle($"{_env} - System Warning")
           .WithDescription(message)
           .WithColor(Color.LightOrange)
           .WithCurrentTimestamp()
           .Build();

            await SendMessage(embed);
        }

        private async Task StartPing()
        {
            var emmbed = new EmbedBuilder()
                .WithTitle($"{_env} - Ping")
                .WithDescription("Hello everybody, i am coming ... ")
                .WithColor(Color.Green)
                .WithCurrentTimestamp()
                .Build();

            await SendMessage(emmbed);
        }

        private async Task EndPing()
        {
            var emmbed = new EmbedBuilder()
                .WithTitle($"{_env} - Pong")
                .WithDescription("See you again")
                .WithColor(Color.DarkGrey)
                .WithCurrentTimestamp()
                .Build();

            await SendMessage(emmbed);
        }

        public void Dispose()
        {
            EndPing().Wait();
            _client?.Dispose();
        }

        ~DiscordService()
        {
            Dispose();
        }
    }
}
