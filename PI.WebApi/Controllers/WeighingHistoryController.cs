using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PI.Domain.Dto.Product;
using PI.Domain.Models;

namespace PI.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/weighing-history")]

    public class WeighingHistoryController : ControllerBase
    {
        private readonly IWeighingHistoryService _weighingHistoryService;

        public WeighingHistoryController(IWeighingHistoryService weighingHistoryService)
        {
            _weighingHistoryService = weighingHistoryService;
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] WeighingHistory req)
        {
            var response = await _weighingHistoryService.Search(req);
            return StatusCode((int)response.StatusCode, response);
        }

        //[Authorize]
        //[HttpGet("{productId}")]
        //public async Task<IActionResult> GetProductDetail(int productId)
        //{
        //    var response = await _weighingHistoryService.GetProductDetail(productId);
        //    return StatusCode((int)response.StatusCode, response);
        //}

        //[Authorize(Roles = "Manager")]
        [HttpPost("create-weighing-history")]
        public async Task<IActionResult> Create(WeighingHistory request)
        {
            var response = await _weighingHistoryService.Create(request);
            return StatusCode((int)response.StatusCode, response);
        }

        //[Authorize(Roles = "Manager")]
        //[HttpPost("create-product-medicine")]
        //public async Task<IActionResult> CreateProductMedicine(CreateProductMedicineRequest request)
        //{
        //    var response = await _weighingHistoryService.CreateMedicine(request);
        //    return StatusCode((int)response.StatusCode, response);
        //}

        //[Authorize(Roles = "Manager")]
        //[HttpPut("update-product/{productId}")]
        //public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductRequest request, int productId)
        //{
        //    var response = await _weighingHistoryService.UpdateProduct(request, productId);
        //    return StatusCode((int)response.StatusCode, response);
        //}

        [Authorize(Roles = "Manager")]
        [HttpPut("update-medicine/{productId}")]
        public async Task<IActionResult> UpdateMedicine(UpdateMedicineRequest request, int productId)
        {
            var response = await _weighingHistoryService.UpdateMedicine(request, productId);
            return StatusCode((int)response.StatusCode, response);
        }

        [Authorize(Roles = "Manager")]
        [HttpDelete("delete-product")]
        public async Task<IActionResult> DeleteProduct([FromQuery] int[] ids)
        {
            var result = await _weighingHistoryService.DeleteProduct(ids);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
