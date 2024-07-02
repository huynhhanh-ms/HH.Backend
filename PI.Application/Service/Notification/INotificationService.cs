using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using PI.Domain.Dto.Notification;

namespace PI.Application.Service.Notification
{
    public interface INotificationService
    {
        //Task SendNotification(NotificationRequest request, int receiverId);
        Task SendNotification(SendNotificationRequest sendNotiReq);
        Task<ApiResponse<IEnumerable<NotificationResponse>>> GetNotificationByReceiverId(int receiverId);
        Task<ApiResponse<string>> UpdateNotificationIsRead(int notificationId);
        Task<SendNotificationRequest> BuildStockCheckNotification(int stockCheckId);
    }
}