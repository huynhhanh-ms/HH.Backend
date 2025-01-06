using HH.Application.Services;
using HH.Domain.Common;
using HH.Domain.Dto;
using HH.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HH.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/tank-history")]
    public class TankHistoryController : ControllerBase
    {
        private readonly ITankHistoryService _service;

        public TankHistoryController(ITankHistoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Gets([FromQuery] SearchBaseRequest param)
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
        public async Task<IActionResult> Update([FromBody] TankHistory request)
        {
            var result = await _service.Update(request);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TankHistoryCreateDto request)
        {
            var result = await _service.Create(request);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveHistory([FromBody] TankHistorySaveDto request)
        {
            var result = await _service.Save(request);
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
