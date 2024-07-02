using FirebaseAdmin.Messaging;
using PI.Domain.Dto.Notification;
using PI.Domain.Enums;
using PI.Domain.Models;
using static PI.Domain.Enums.StockCheckEnum;

namespace PI.Application.Service.Notification.NotificationBuilders
{
    public class StockCheckNotificationBuilder : INotificationDecorator<StockCheck>
    {
        private readonly INotificationBuilder _notificationBuilder;
        private StockCheck? _stockCheck = null;

        public StockCheckNotificationBuilder(INotificationBuilder notificationBuilder)
        {
            _notificationBuilder = notificationBuilder;
        }

        public SendNotificationRequest Build()
        {
            return _notificationBuilder.Build();
        }

        public INotificationDecorator<StockCheck> AddObject(StockCheck obj)
        {
            _stockCheck = obj;

            return this;
        }

        public INotificationBuilder AddPayload(string? title = null)
        {
            if (!string.IsNullOrEmpty(title))
            {
                _notificationBuilder.AddPayload(title);
                return this;
            }

            _notificationBuilder.AddPayload(_stockCheck.StockCheckId.ToString());

            return this;
        }

        public INotificationBuilder AddBody(string? body)
        {
            if (!string.IsNullOrEmpty(body))
            {
                _notificationBuilder.AddBody(body);
                return this;
            }

            switch (Enum.Parse<StockCheckStatus>(_stockCheck.Status))
            {
                case StockCheckStatus.Todo:
                    body = "You have a new stock check";
                    break;
                case StockCheckStatus.Assigned:
                    body = "You have been assigned a stock check";
                    break;
                case StockCheckStatus.Accepted:
                    body = "You have accepted a stock check";
                    break;
                case StockCheckStatus.AssignmentDeclined:
                    body = "You have rejected a stock check";
                    break;
                case StockCheckStatus.Submitted:
                    body = "You have submitted a stock check";
                    break;
                case StockCheckStatus.Confirmed:
                    body = "You have confirmed a stock check";
                    break;
                case StockCheckStatus.Completed:
                    body = "You have rejected a stock check";
                    break;
                case StockCheckStatus.Rejected:
                    body = "You have completed a stock check";
                    break;
            }
            _notificationBuilder.AddBody(body);
            return this;
        }

        public INotificationBuilder AddTitle(string? title = null)
        {
            if (!string.IsNullOrEmpty(title))
            {
                _notificationBuilder.AddTitle(title);
                return this;
            }

            switch (Enum.Parse<StockCheckStatus>(_stockCheck.Status))
            {
                case StockCheckStatus.Todo:
                    title = "Stock check created";
                    break;
                case StockCheckStatus.Assigned:
                    title = "Stock check assigned";
                    break;
                case StockCheckStatus.Accepted:
                    title = "Stock check accepted";
                    break;
                case StockCheckStatus.AssignmentDeclined:
                    title = "Stock check declined";
                    break;
                case StockCheckStatus.Submitted:
                    title = "Stock check submmited";
                    break;
                case StockCheckStatus.Confirmed:
                    title = "Stock check confirmed";
                    break;
                case StockCheckStatus.Completed:
                    title = "Stock check Completed";
                    break;
                case StockCheckStatus.Rejected:
                    title = "Stock check rejected";
                    break;
            }

            _notificationBuilder.AddTitle(title);
            return this;
        }

        public INotificationBuilder AddType(string? type)
        {
            if (!string.IsNullOrEmpty(type))
            {
                _notificationBuilder.AddType(type);
                return this;
            }

            switch (Enum.Parse<StockCheckStatus>(_stockCheck.Status))
            {
                case StockCheckStatus.Todo:
                    type = StockCheckNotificationType.CREATE.ToString();
                    break;
                case StockCheckStatus.Assigned:
                    type = StockCheckNotificationType.ASSIGN.ToString();
                    break;
                case StockCheckStatus.Accepted:
                    type = StockCheckNotificationType.ACCEPT_ASSIGN.ToString();
                    break;
                case StockCheckStatus.AssignmentDeclined:
                    type = StockCheckNotificationType.REJECT_ASSIGN.ToString();
                    break;
                case StockCheckStatus.Submitted:
                    type = StockCheckNotificationType.SUBMIT.ToString();
                    break;
                case StockCheckStatus.Confirmed:
                    type = StockCheckNotificationType.CONFIRM.ToString();
                    break;
                case StockCheckStatus.Completed:
                    type = StockCheckNotificationType.COMPLETE.ToString();
                    break;
                case StockCheckStatus.Rejected:
                    type = StockCheckNotificationType.REJECT.ToString();
                    break;
            }

            _notificationBuilder.AddType(type);
            return this;
        }

        public INotificationBuilder AddReceivers(params int[] receiverIds)
        {
            if (receiverIds.Length > 0)
            {
                _notificationBuilder.AddReceivers(receiverIds);
            }

            _notificationBuilder.AddReceivers(_stockCheck.CreatedBy);

            if (_stockCheck.StaffId != null)
                _notificationBuilder.AddReceivers(_stockCheck.StaffId ?? -1);

            if (_stockCheck.StockkeeperId != null)
                _notificationBuilder.AddReceivers(_stockCheck.StockkeeperId ?? -1);

            return this;
        }

        public INotificationBuilder AddDate(string? data = null)
        {
            _notificationBuilder.AddDate(data);
            return this;
        }
    }
}
