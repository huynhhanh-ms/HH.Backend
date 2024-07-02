using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PI.Application.Service.Authen;
using PI.Domain.Dto.Authen;
using System.Net;

namespace PI.WebApi.Controllers
{

    [ApiController]
    [Route("api/v1/auth")]
    public class AuthenController : ControllerBase
    {
        private readonly IAuthenService _authenService;
        
        public AuthenController(IAuthenService authenService)
        {
            _authenService = authenService;
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
            var result = await _authenService.Login(request);
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
            return StatusCode((int) result.StatusCode, result);
        }
        
        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            //get access token from cookie
            var result = await _authenService.RefreshToken(request.AccessToken);
            if (result is { StatusCode: HttpStatusCode.OK, Data : not null})
            {
                //set cookie
                Response.Cookies.Append("access_token", result.Data.AccessToken, new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddHours(AppConfig.JwtSetting.AccessTokenExpiration),
                    SameSite = SameSiteMode.Strict,
                    Secure = true
                });
            } else {
                //remove cookie
                Response.Cookies.Delete("access_token");
            }
            return StatusCode((int) result.StatusCode, result);
        }
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetAuthenticatedAccount()
        {
            var result = await _authenService.GetCurrentAccount();
            return StatusCode((int) result.StatusCode, result);
        }
        
        [HttpDelete("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            //get access token from header request
            await _authenService.Logout();
            //remove cookie
            Response.Cookies.Delete("access_token");
            return StatusCode((int) HttpStatusCode.OK, "Logout successfully!");
        }
        
    }
}