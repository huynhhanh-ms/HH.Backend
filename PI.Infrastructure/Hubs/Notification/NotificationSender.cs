using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using PI.Domain.Dto.Notification;
using PI.Domain.Enums;
using PI.Domain.Infrastructure.Hub;

namespace PI.Infrastructure.Hubs.Notification
{
    public class NotificationSender : INotificationSender
    {
        
        private readonly IHubContext<NotificationHub> _hubContext;
        
        public NotificationSender(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }
        
        public async Task SendToAllAsync(NotificationRequest notificationRequest)
        {
            // var objJsonString = JsonConvert.SerializeObject(notificationRequest);
            await _hubContext.Clients.All.SendAsync(SignalRType.ReceiveNotification.ToString(), notificationRequest);
        }
        
        public async Task SendToUserAsync(string userId, NotificationRequest notificationRequest)
        {
            // var objJsonString = JsonConvert.SerializeObject(notificationRequest);
            await _hubContext.Clients.User(userId).SendAsync(SignalRType.ReceiveNotification.ToString(), notificationRequest);
        }
        
        public async Task SendToUserAsync(IEnumerable<string> userIds, NotificationRequest notificationRequest)
        {
            // var objJsonString = JsonConvert.SerializeObject(notificationRequest);
            await _hubContext.Clients.Users(userIds).SendAsync(SignalRType.ReceiveNotification.ToString(), notificationRequest);
        }
        
    }
}