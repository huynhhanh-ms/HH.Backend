using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PI.Application.Service.Notification;
using PI.Domain.Dto.Notification;

namespace PI.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/notification")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService  _notificationService;
        

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        
        //send notification to user
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> SendNotification([FromBody] SendNotificationRequest request)
        {
            await _notificationService.SendNotification(request);
            return Ok();
        }
        
        [HttpGet("{receiverId}")]
        public async Task<IActionResult> GetNotificationByReceiverId(int receiverId)
        {
            var result = await _notificationService.GetNotificationByReceiverId(receiverId);
            return StatusCode((int)result.StatusCode, result);
        }
        
        //update notification is read
        [HttpPut("{notificationId}")]
        public async Task<IActionResult> UpdateNotificationIsRead(int notificationId)
        {
            var result = await _notificationService.UpdateNotificationIsRead(notificationId);
            return StatusCode((int)result.StatusCode, result);
        }
        
    }
}