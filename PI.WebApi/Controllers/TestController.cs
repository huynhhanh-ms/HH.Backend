using Discord;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PI.Application.Service.Assignments;
using PI.Application.Service.Notification;
using PI.Domain.BackgroundQueue;
using PI.Domain.Dto.Assignment;
using PI.Domain.Infrastructure.Discord;
using PI.Infrastructure.Discord;

namespace PI.WebApi.Controllers
{
    [Route("api/v1/test")]
    [ApiController]
    public class TestController(
        IServiceScopeFactory serviceScopeFactory,
        IDiscordService discordService
        ) : ControllerBase
    {


        [HttpGet("discord/ping")]
        public async Task<IActionResult> Get()
        {
            await discordService.SendInfoMesssage("Ping pong");

            await Task.CompletedTask;

            return Ok();
        }


        private async Task DosomThings()
        {
            var scope = serviceScopeFactory.CreateAsyncScope();
            var service = scope.ServiceProvider.GetRequiredService<INotificationService>();

            await service.SendNotification(new Domain.Dto.Notification.SendNotificationRequest
            {
            });
        }
    }
}
