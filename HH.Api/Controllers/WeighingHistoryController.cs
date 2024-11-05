using HH.Application.Services;
using HH.Domain.Common;
using HH.Domain.Dto;
using HH.Domain.Dto.WeighingHistory;
using HH.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HH.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/weighing-history")]
    public class WeighingHistoryController : ControllerBase
    {
        private readonly IWeighingHistoryService _service;

        public WeighingHistoryController(IWeighingHistoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Gets([FromQuery] WeighingHistorySearch param)
        {
            var result = await _service.Gets(param);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.Get(id);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] WeighingHistoryUpdateDto request)
        {
            var result = await _service.Update(request);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WeighingHistoryCreateDto request)
        {
            var result = await _service.Create(request);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
