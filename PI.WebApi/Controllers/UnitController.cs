using Microsoft.AspNetCore.Mvc;

namespace PI.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/unit")]
    public class UnitController : ControllerBase
    {
        private readonly IUnitService _unitService;

        public UnitController(IUnitService unitService)
        {
            _unitService = unitService;
        }

        [HttpGet]
        public async Task<IActionResult> SearchUnit([FromQuery] SearchBaseRequest request)
        {
            var response = await _unitService.SearchUnit(request.KeySearch, request.PagingQuery, request.OrderBy);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}