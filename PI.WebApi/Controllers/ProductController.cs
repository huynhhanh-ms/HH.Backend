using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PI.Domain.Dto.Product;

namespace PI.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> SearchProduct([FromQuery] SearchProductRequest req)
        {
            var response = await _productService.SearchProduct(req);
            return StatusCode((int)response.StatusCode, response);
        }

        [Authorize]
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductDetail(int productId)
        {
            var response = await _productService.GetProductDetail(productId);
            return StatusCode((int)response.StatusCode, response);
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("create-product")]
        public async Task<IActionResult> CreateProduct(CreateProductRequest request)
        {
            var response = await _productService.CreateProduct(request);
            return StatusCode((int)response.StatusCode, response);
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("create-product-medicine")]
        public async Task<IActionResult> CreateProductMedicine(CreateProductMedicineRequest request)
        {
            var response = await _productService.CreateMedicine(request);
            return StatusCode((int)response.StatusCode, response);
        }

        [Authorize(Roles = "Manager")]
        [HttpPut("update-product/{productId}")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductRequest request, int productId)
        {
            var response = await _productService.UpdateProduct(request, productId);
            return StatusCode((int)response.StatusCode, response);
        }

        [Authorize(Roles = "Manager")]
        [HttpPut("update-medicine/{productId}")]
        public async Task<IActionResult> UpdateMedicine(UpdateMedicineRequest request, int productId)
        {
            var response = await _productService.UpdateMedicine(request, productId);
            return StatusCode((int)response.StatusCode, response);
        }

        [Authorize(Roles = "Manager")]
        [HttpDelete("delete-product")]
        public async Task<IActionResult> DeleteProduct([FromQuery] int[] ids)
        {
            var result = await _productService.DeleteProduct(ids);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}