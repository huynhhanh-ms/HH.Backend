using HH.Application.Services;
using HH.Domain.Common;
using HH.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HH.Api.Controllers
{
    [ApiController]
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
        public async Task<IActionResult> Search([FromQuery] SearchBaseRequest param)
        {
            var result = await _service.Search(param);
            return StatusCode((int)result.StatusCode, result);
        }

    }
}
