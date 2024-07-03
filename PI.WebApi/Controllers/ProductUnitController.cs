using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PI.Domain.Dto.Product;

namespace PI.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/product-unit")]
    [ApiExplorerSettings(IgnoreApi = true)]

    public class ProductUnitController : ControllerBase
    {
        private readonly IProductUnitService _productUnitService;

        public ProductUnitController(IProductUnitService productUnitService)
        {
            _productUnitService = productUnitService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> SearchProductUnit([FromQuery] SearchBaseRequest searchBaseReq)
        {
            var response = await _productUnitService.SearchProductUnit(searchBaseReq.KeySearch,
                searchBaseReq.PagingQuery, searchBaseReq.OrderBy);
            return StatusCode((int)response.StatusCode, response);
        }

        [Authorize]
        [HttpGet("{skuCode}")]
        public async Task<IActionResult> GetProductUnitBySkuCode(string skuCode)
        {
            var response = await _productUnitService.GetProductUnitBySkuCode(skuCode);
            return StatusCode((int)response.StatusCode, response);
        }

        [Authorize]
        [HttpGet("history/{skuCode}")]
        public async Task<IActionResult> GetProductHistory(string skuCode, [FromQuery] SearchProductHistory searchReq)
        {
            var response = await _productUnitService.GetProductHistory(skuCode, searchReq);
            return StatusCode((int)response.StatusCode, response);
        }
        
        [Authorize]
        [HttpGet("statistic/{skuCode}")]
        public async Task<IActionResult> StatisticsProductUnit(string skuCode, [FromQuery] SearchProductStatisticReq req)
        {
            var response = await _productUnitService.StatisticProductUnit(skuCode, req);
            return StatusCode((int)response.StatusCode, response);
        }

        [Authorize]
        [HttpGet("lot/{skuCode}")]
        public async Task<IActionResult> GetProductLot(string skuCode, [FromQuery] SearchProductLot req)
        {
            var response = await _productUnitService.GetProductLot(skuCode, req.PagingQuery, req.OrderBy, req.LotStatus);
            return StatusCode((int)response.StatusCode, response);
        }
        
        [Authorize]
        [HttpDelete("{productUnitId}")]
        public async Task<IActionResult> DeleteProductUnit(int productUnitId)
        {
            var response = await _productUnitService.DeleteProductUnit(productUnitId);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}