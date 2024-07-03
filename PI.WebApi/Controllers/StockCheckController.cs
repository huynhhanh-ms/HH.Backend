using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PI.Application.Service.Notification;
using PI.Application.Service.ProductStockCheck;
using PI.Application.Service.StockBalance;
using PI.Domain.BackgroundQueue;
using PI.Domain.Dto.StockCheck;
using PI.Domain.Extensions;
using PI.Domain.Models;
using System.Net;
using System.Runtime.InteropServices;

namespace PI.WebApi.Controllers
{
    [Route("api/stock-check")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]

    public class StockCheckController : ControllerBase
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IBackgroundTaskQueue _backgroundTaskQueue;

        private readonly IStockCheckService _stockCheckService;

        public StockCheckController(
            IServiceScopeFactory serviceScopeFactory,
            IBackgroundTaskQueue backgroundTaskQueue,
            IStockCheckService stockCheckService
        )
        {
            _serviceScopeFactory = serviceScopeFactory;
            _stockCheckService = stockCheckService;
            _backgroundTaskQueue = backgroundTaskQueue;
        }

        private Task SendStateUpdatedNotification(HttpStatusCode statusCode, int stockCheckId)
        {
            if (statusCode != HttpStatusCode.OK)
            {
                return Task.CompletedTask;
            }

            return Task.Run(async () =>
            {
                try
                {
                    using var scope = _serviceScopeFactory.CreateAsyncScope();
                    var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

                    var sendNotiReq = await notificationService.BuildStockCheckNotification(stockCheckId);
                    await notificationService.SendNotification(sendNotiReq);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.GetExceptionMessage());
                }
            });
        }

        /// <summary>
        /// Create stock check - (only manager)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> CreateStockCheck(CreateStockCheckRequest request)
        {
            var result = await _stockCheckService.CreateStockCheck(request);

            // Send notification to client
            _ = SendStateUpdatedNotification(result.StatusCode, result.Data).ConfigureAwait(false);

            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Update information of a stock check - (only manager)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "Manager, Stockkeeper")]
        public async Task<IActionResult> UpdateStockCheck(UpdateStockCheckRequest request)
        {
            var result = await _stockCheckService.UpdateStockCheck(request);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Assign a staff for a stock check (only manager)
        /// </summary>
        /// <param name="stockCheckId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{stockCheckId}/assign")]
        [Authorize(Roles = "Manager, Stockkeeper")]
        public async Task<IActionResult> AssignStockCheck(int stockCheckId, [FromBody] AssignStockCheckRequest request)
        {
            var res = await _stockCheckService.AssignStockCheck(stockCheckId, request.StaffId);

            // Send notification to client
            _ = SendStateUpdatedNotification(res.StatusCode, stockCheckId).ConfigureAwait(false);

            return StatusCode((int)res.StatusCode, res);
        }

        /// <summary>
        /// Accept a stock check assignment in which you were assigned (only staff)
        /// </summary>
        /// <param name="stockCheckId"></param>
        /// <returns></returns>
        [HttpPut("{stockCheckId}/accept-stock-check-assignmennt")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> AcceptStockCheckAssignment(int stockCheckId)
        {
            var res = await _stockCheckService.AcceptStockCheckAssignmennt(stockCheckId);

            // Send notification to client
            _ = SendStateUpdatedNotification(res.StatusCode, stockCheckId).ConfigureAwait(false);

            return StatusCode((int)res.StatusCode, res);
        }

        /// <summary>
        /// Reject a stock check assignment in which you were assigned (only staff)
        /// </summary>
        /// <param name="stockCheckId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{stockCheckId}/decline-stock-check-assignmennt")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> DeclineStockCheckAssignment(int stockCheckId, [FromBody] DeclineStockCheckAssignmentRequest request)
        {
            var res = await _stockCheckService.DeclineStockCheckAssignmennt(stockCheckId, request);

            // Send notification to client
            _ = SendStateUpdatedNotification(res.StatusCode, stockCheckId).ConfigureAwait(false);

            return StatusCode((int)res.StatusCode, res);
        }

        /// <summary>
        /// Submit data of a stock check (only staff)
        /// </summary>
        /// <param name="isDraft"></param>
        /// <param name="stockCheckId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{stockCheckId}/submit")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> SubmitStockCheck(
            [FromRoute] int stockCheckId,
            [FromQuery] bool isDraft,
            [FromBody] SubmitStockCheckRequest request)
        {
            var res = await _stockCheckService.SubmitStockCheck(stockCheckId, request, isDraft);

            // Send notification to client
            _ = SendStateUpdatedNotification(res.StatusCode, stockCheckId).ConfigureAwait(false);

            return StatusCode((int)res.StatusCode, res);
        }


        /// <summary>
        /// Confirm a stock check (only stockkeeper)
        /// </summary>
        /// <param name="stockCheckId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{stockCheckId}/confirm")]
        [Authorize(Roles = "Stockkeeper")]
        public async Task<IActionResult> ConfirmStockCheck(int stockCheckId, [FromBody] ConfirmStockCheckRequest request)
        {
            var res = await _stockCheckService.ConfirmStockCheck(stockCheckId, request);

            // Send notification to client
            _ = SendStateUpdatedNotification(res.StatusCode, stockCheckId).ConfigureAwait(false);

            return StatusCode((int)res.StatusCode, res);
        }

        /// <summary>
        /// Reject a stock check (stockkeeper, manager)
        /// </summary>
        /// <param name="stockCheckId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{stockCheckId}/reject")]
        [Authorize(Roles = "Stockkeeper,Manager")]
        public async Task<IActionResult> RejectStockCheck(int stockCheckId, [FromBody] RejectStockCheckRequest request)
        {
            var res = await _stockCheckService.RejectStockCheck(stockCheckId, request);

            // Send notification to client
            _ = SendStateUpdatedNotification(res.StatusCode, stockCheckId).ConfigureAwait(false);

            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPut("{stockCheckId}/complete")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> CompleteStockCheck(int stockCheckId, [FromBody] CompleteStockCheckRequest request)
        {

            var res = await _stockCheckService.CompleteStockCheck(stockCheckId, request);

            // Send notification to client
            _ = SendStateUpdatedNotification(res.StatusCode, stockCheckId).ConfigureAwait(false);

            //Balance stock and send notification
            _ = _backgroundTaskQueue.QueueBackgroundTaskAsync(async (token) =>
            {
                Console.WriteLine("Background queue running");
                using var scope = _serviceScopeFactory.CreateScope();
                var stockBalanceService = scope.ServiceProvider.GetRequiredService<IStockBalanceService>();
                var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

                var stockBalanceRes = await stockBalanceService.BalanceStockByStockCheck(stockCheckId);

                if (stockBalanceRes.StatusCode == HttpStatusCode.OK)
                {
                    var sendNotiReq = await notificationService.BuildStockCheckNotification(stockCheckId);
                    await notificationService.SendNotification(sendNotiReq);
                }
            });

            return StatusCode((int)res.StatusCode, res);
        }

        /// <summary>
        /// Delete a stock check (only manager)
        /// </summary>
        /// <param name="stockCheckId"></param>
        /// <returns></returns>
        [Authorize(Roles = "Manager")]
        [HttpDelete("{stockCheckId}/delete")]
        public async Task<IActionResult> DeleteStockCheck(int stockCheckId)
        {
            var result = await _stockCheckService.DeleteStockCheck(stockCheckId);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Search stock check ; keysearch : stock check id, title
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> SearchStockCheck([FromQuery] SearchStockCheckRequest request)
        {
            var result = await _stockCheckService.SearchStockCheck(request);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Search stock check details of a stock check
        /// </summary>
        /// <param name="stockCheckId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        //[Authorize]
        [HttpGet("{stockCheckId}/stock-check-details")]
        public async Task<IActionResult> SearchStockCheckDetail(int stockCheckId, [FromQuery] SearchStockCheckDetailRequest request)
        {
            var result = await _stockCheckService.SearchStockCheckDetail(stockCheckId, request);

            return StatusCode((int)result.StatusCode, result);
        }
    }
}
