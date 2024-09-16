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
        private readonly HhDatabaseContext _context;

        public AccountController(HhDatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create account staff for system (only manager can do this)
        /// </summary>
        /// <param name="request"></param>
        /// <returns> response </returns>
        [HttpGet("get")]
        //[Authorize(Roles = "Manager, Admin")]
        public async Task<IActionResult> GetAccount()
        {
            var result = await _context.Accounts.ToListAsync();
            return Ok(result);
        }
    }
}
