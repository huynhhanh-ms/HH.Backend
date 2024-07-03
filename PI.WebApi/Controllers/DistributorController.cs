using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PI.Domain.Dto.Distributor;

namespace PI.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/distributor")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class DistributorController : ControllerBase
    {
        private readonly IDistributorService _distributorService;

        public DistributorController(IDistributorService distributorService)
        {
            _distributorService = distributorService;
        }

        [HttpPost]
        [Authorize(Roles = "Manager, Stockkeeper")]
        public async Task<IActionResult> Create([FromBody] CreateDistributorRequest request)
        {
            var result = await _distributorService.Create(request);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Search([FromQuery] SearchBaseRequest searchBaseReq)
        {
            var result = await _distributorService.Search(searchBaseReq.KeySearch, searchBaseReq.PagingQuery,
                searchBaseReq.OrderBy);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}