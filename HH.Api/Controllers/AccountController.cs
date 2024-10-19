using HH.Application.Services;
using HH.Domain.Common;
using HH.Domain.Dto;
using HH.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HH.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService accountService)
        {
            _service = accountService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //[Authorize(Roles = "")]
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
        public async Task<IActionResult> Update([FromBody] Account request)
        {
            var result = await _service.Update(request);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AccountCreateDto request)
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
