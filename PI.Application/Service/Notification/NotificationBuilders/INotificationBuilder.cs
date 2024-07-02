using PI.Domain.Dto.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Application.Service.Notification.NotificationBuilders
{
    public interface INotificationBuilder
    {
        INotificationBuilder AddTitle(string? title = null);
        INotificationBuilder AddBody(string? body = null);
        INotificationBuilder AddPayload(string? payload = null);
        INotificationBuilder AddType(string? type = null);
        INotificationBuilder AddDate(string? data = null);
        INotificationBuilder AddReceivers(params int[]receiverIds);
        SendNotificationRequest Build();
    }
}
