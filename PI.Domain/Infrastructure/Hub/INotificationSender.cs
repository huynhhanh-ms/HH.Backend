using PI.Domain.Dto.Notification;

namespace PI.Domain.Infrastructure.Hub
{
    public interface INotificationSender
    {
        Task SendToAllAsync(NotificationRequest notificationRequest);
        Task SendToUserAsync(string userId, NotificationRequest notificationRequest);
        Task SendToUserAsync(IEnumerable<string> userIds, NotificationRequest notificationRequest);
    }
}