using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PI.Domain.Enums;


namespace PI.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Create account staff for system (only manager can do this)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create-staff")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> CreateStaffAccount([FromBody] RegisterAccountRequest request)
        {
            var result = await _accountService.CreateStaffAccount(request);
            return StatusCode((int)result.StatusCode, result);
        }

        /// <summary>
        /// Get information of staff
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("staff-info")]
        [Authorize(Roles = "Manager, Stockkeeper")]
        public async Task<IActionResult> SearchAccountStaff([FromQuery] SearchStaffReq request)
        {
            var result = await _accountService.SearchAccountStaff(request);
            return StatusCode((int)result.StatusCode, result);
        }
        
        /// <summary>
        /// Search account by role
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Manager, Stockkeeper")]
        public async Task<IActionResult> SearchAccountByRole([FromQuery] SearchAccountReq request)
        {
            var result = await _accountService.SearchAccountByRole(request);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut("status/{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> UpdateAccountStatus([FromRoute] int id,
            [FromBody] UpdateAccountStatusRequest request)
        {
            var result = await _accountService.UpdateStatusAccount(id, request);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut("info/{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> UpdateAccountInfo([FromRoute] int id, [FromBody] UpdateAccountRequest request)
        {
            var result = await _accountService.UpdateAccountInfo(id, request);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteAccount([FromRoute] int id)
        {
            var result = await _accountService.Delete(id);
            return StatusCode((int)result.StatusCode, result);
        }
        
    }
}
