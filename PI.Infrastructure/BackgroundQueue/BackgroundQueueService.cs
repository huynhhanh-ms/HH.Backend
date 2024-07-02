using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using PI.Domain.BackgroundQueue;
using PI.Domain.Infrastructure.Discord;

namespace PI.Infrastructure.BackgroundQueue
{
    public class BackgroundQueueService(
        IBackgroundTaskQueue taskQueue,
        IDiscordService discordService
    ) : BackgroundService
    {
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            //Console.WriteLine("BackgroundQueueService is starting.");
            await discordService.SendInfoMesssage("Background queue running ...");
            await base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            //Console.WriteLine("BackgroundQueueService is stopping.");
            await discordService.SendInfoMesssage("Background queue stopped ...");
            await base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    Func<CancellationToken, ValueTask> task = await taskQueue.DequeueAsync(stoppingToken);

                    if (task == null)
                        continue;

                    await task(stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    // Prevent throwing if stoppingToken was signaled
                }
                catch (Exception ex)
                {
                    await discordService.SendInfoMesssage("Background queue exception: " + JsonConvert.SerializeObject(ex, Formatting.Indented));
                }
            }
        }


    }
}
