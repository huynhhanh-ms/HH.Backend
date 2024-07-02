using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PI.Domain.Dto.Category;

namespace PI.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/category-limit")]
    public class CategoryLimitController : ControllerBase
    {
        private readonly ICategoryLimitService _categoryLimitService;

        public CategoryLimitController(ICategoryLimitService categoryLimitService)
        {
            _categoryLimitService = categoryLimitService;
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _categoryLimitService.GetAll();
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Create([FromBody] CategoryLimitRequest request)
        {
            var result = await _categoryLimitService.Create(request);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}