using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PI.Application.Service.StockBalance;
using PI.Domain.Dto.StockCheck;

namespace PI.WebApi.Controllers
{
    [Route("api/stock-balance")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]

    public class StockBalanceController : ControllerBase
    {
        private readonly IStockBalanceService _stockBalanceService;

        public StockBalanceController(IStockBalanceService stockBalanceService)
        {
            _stockBalanceService = stockBalanceService;
        }

        // <summary>
        /// Search stock check ; keysearch : not used
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> SearchStockBalance([FromQuery] SearchStockCheckRequest request)
        {
            var result = await _stockBalanceService.SearchStockBalance(request);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Use balance stock check
        /// </summary>
        /// <param name="stockCheckId"></param>
        /// <returns></returns>
        [Authorize(Roles = "Manager")]
        [HttpPut("{stockCheckId}/use-balance")]
        public async Task<IActionResult> UseBalanceStockCheck(int stockCheckId)
        {
            var result = await _stockBalanceService.BalanceStockByStockCheck(stockCheckId);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}