using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PI.Domain.Dto.ProductStock;
using PI.Domain.Dto.ProductStock.ProductStockEstimate;

namespace PI.WebApi.Controllers
{
    [Route("api/v1/product-stock")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]

    public class ProductStockController : ControllerBase
    {
        private readonly IProductStockService _productStockService;

        public ProductStockController(IProductStockService productStockService)
        {
            _productStockService = productStockService;
        }

        /// <summary>
        /// Get stock quantity of products by export request id
        /// </summary>
        /// <param name="exportRequestId"></param>
        /// <returns></returns>
        
        
        [HttpGet("export-request/{exportRequestId}")]
        public async Task<IActionResult> GetProductStockReponseByExportRequestId(int exportRequestId)
        {
            var response = await _productStockService.GetProductStockReponseByExportRequestId(exportRequestId);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Get estimate stock quantity of products (estimate quantity = current stock quantity - export request quantity)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("estimate")]
        public async Task<IActionResult> GetProductStockEstimate([FromQuery]SearchProductStockEstimateRequest request)
        {
            var response = await _productStockService.SearchEstProductStock(request);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Get estimate stock quantity of products (estimate quantity = current stock quantity + import request quantity - export request quantity)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("estimate/import")]
        public async Task<IActionResult> GetProductStockEstimateWithImportRequest([FromQuery] SearchProductStockEstimateRequest request)
        {
            var response = await _productStockService.SearchEstProductStockWithImportRequest(request);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Check stock for export request
        /// </summary>
        /// <param name="exportRequestId"></param>
        /// <returns></returns>
        [HttpPost("export-request/{exportRequestId}/check-stock")]
        public async Task<IActionResult> CheckStockByExportRequest(int exportRequestId)
        {
            var response = await _productStockService.CheckStock4ExportRequest(exportRequestId);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Search product stock - key search is SkuCode
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> SearchProductStock([FromQuery]SearchProductStockRequest request)
        {
            var response = await _productStockService.SearchProductStock(request);
            return StatusCode((int)response.StatusCode, response);
        }

        
    }
}
