using HH.Application.Services;
using HH.Domain.Common;
using HH.Domain.Dto;
using HH.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HH.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/expense-type")]
    public class ExpenseTypeController : ControllerBase
    {
        private readonly IExpenseTypeService _service;

        public ExpenseTypeController(IExpenseTypeService service)
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
        public async Task<IActionResult> Update([FromBody] ExpenseTypeUpdateDto request)
        {
            var result = await _service.Update(request);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ExpenseTypeCreateDto request)
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
