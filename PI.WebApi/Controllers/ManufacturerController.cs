using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PI.Domain.Dto.Manufacturer;

namespace PI.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/manufacturer")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ManufacturerController : ControllerBase
    {
        private readonly IManufacturerService _manufacturerService;

        public ManufacturerController(IManufacturerService manufacturerService)
        {
            _manufacturerService = manufacturerService;
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateManufacturerRequest request)
        {
            var result = await _manufacturerService.Create(request);
            return StatusCode((int)result.StatusCode, result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _manufacturerService.GetAll();
            return StatusCode((int)result.StatusCode, result);
        }
    }
}