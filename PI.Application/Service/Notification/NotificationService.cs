using Autofac;
using Microsoft.Extensions.DependencyInjection;
using PI.Application.Service.Notification.NotificationBuilders;
using PI.Domain.Dto.Notification;
using PI.Domain.Infrastructure.Hub;
using PI.Domain.Models;
using static PI.Domain.Enums.StockCheckEnum;

namespace PI.Application.Service.Notification
{
    public class NotificationService : BaseService, INotificationService
    {
        private readonly INotificationSender _notificationSender;
        private readonly INotificationDecorator<StockCheck> _stockCheckNotificationBuilder;

        public NotificationService(
            IUnitOfWork unitOfWork,
            INotificationSender notificationSender,
            INotificationDecorator<StockCheck> notificationBuilder
        ) : base(unitOfWork)
        {
            _notificationSender = notificationSender;
            _stockCheckNotificationBuilder = notificationBuilder;
        }

        public async Task SendNotification(SendNotificationRequest sendNotiReq)
        {
            //using IServiceScope scope = _serviceScopeFactory.CreateAsyncScope();
            //var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                foreach (var id in sendNotiReq.ReceiverIds)
                {
                    var notification = sendNotiReq.Notification.Adapt<Domain.Models.Notification>();
                    notification.ReceiverId = id;
                    //send notification to user
                    await _unitOfWork.Resolve<Domain.Models.Notification>().CreateAsync(notification);
                    await _unitOfWork.SaveChangesAsync();
                }

                await _unitOfWork.CommitTransactionAsync();
                await _notificationSender.SendToUserAsync(sendNotiReq.ReceiverIds.Select(p => p.ToString()), sendNotiReq.Notification);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.GetExceptionMessage());
            }
        }

        //get notification by receiverIds
        public async Task<ApiResponse<IEnumerable<NotificationResponse>>> GetNotificationByReceiverId(int receiverId)
        {
            try
            {
                var notifications = await _unitOfWork.Resolve<Domain.Models.Notification>()
                    .FindListAsync(p => p.ReceiverId == receiverId);
                return Success(notifications.Adapt<IEnumerable<NotificationResponse>>());
            }
            catch (Exception ex)
            {
                return Failed<IEnumerable<NotificationResponse>>(ex.GetExceptionMessage());
            }
        }

        //update notification is read
        public async Task<ApiResponse<string>> UpdateNotificationIsRead(int notificationId)
        {
            try
            {
                var notification = await _unitOfWork.Resolve<Domain.Models.Notification>().FindAsync(notificationId);
                if (notification == null)
                {
                    return Failed<string>("Notification not found!");
                }

                notification.IsRead = true;
                await _unitOfWork.SaveChangesAsync();
                return Success<string>("Update notification is read successfully!");
            }
            catch (Exception ex)
            {
                return Failed<string>(ex.GetExceptionMessage());
            }
        }

        public async Task<SendNotificationRequest> BuildStockCheckNotification(
            int stockCheckId
        )
        {
            //using IServiceScope scope = _serviceScopeFactory.CreateAsyncScope();
            //var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var stockCheck = await _unitOfWork.Resolve<StockCheck>().FindAsync(stockCheckId);

            _stockCheckNotificationBuilder
               .AddObject(stockCheck);

            if (stockCheck.IsUsedForBalancing)
            {
                _stockCheckNotificationBuilder
                    .AddTitle("Kho đã đc cân bằng")
                    .AddBody($"Đợt kiểm kho #{stockCheckId} đã đc dùng để cân bằng kho");
            }
            else
            {
                _stockCheckNotificationBuilder
                    .AddTitle()
                    .AddBody();
            }

            _stockCheckNotificationBuilder
               .AddPayload()
               .AddType();

            var stockkeepers = await _unitOfWork.Resolve<Account>()
                    .FindListAsync(a => a.Role == AccountRole.Stockkeeper.ToString());
            _stockCheckNotificationBuilder.AddReceivers(stockkeepers.Select(x => x.AccountId).ToArray());

            return _stockCheckNotificationBuilder.Build();
        }
    }
}