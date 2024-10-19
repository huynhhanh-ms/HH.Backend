using HH.Application.Services;
using HH.Domain.Common;
using HH.Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using HH.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace HH.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/session")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _service;

        public SessionController(ISessionService service)
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
        public async Task<IActionResult> Update([FromBody] SessionUpdateDto request)
        {
            var result = await _service.Update(request);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SessionCreateDto request)
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

        [HttpPut("close")]
        public async Task<IActionResult> Close([FromBody] int id)
        {
            var result = await _service.Close(id);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
