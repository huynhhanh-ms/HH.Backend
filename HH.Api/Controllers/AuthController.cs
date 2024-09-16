using HH.Application.Services;
using HH.Domain.Dto.Authen;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HH.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        public AuthController(IAuthService service)
        {
            _service = service;
        }

        /// <summary>
        /// Login to system with username and password
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _service.Login(request);
            if (result is { StatusCode: HttpStatusCode.OK, Data: not null })
            {
                //set cookie
                Response.Cookies.Append("access_token", result.Data.AccessToken, new CookieOptions
                {
                    HttpOnly = true,
                    Expires = result.Data.ExpirationTime,
                    SameSite = SameSiteMode.Strict,
                    Secure = true
                });
            }
            return StatusCode((int)result.StatusCode, result);
        }

    }
}
