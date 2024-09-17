using System.Security.Claims;
using HH.Application.Common;
using HH.Application.Services;
using HH.Domain.Infrastructure.Auth;

namespace HH.Api.Middleware
{
    public class CurrentAccountMiddleware : IMiddleware
    {
        private readonly IAuthService _authenService;
        private readonly ICurrentAccount _currentAccount;

        public CurrentAccountMiddleware(IAuthService authenService, ICurrentAccount currentAccount)
        {
            _authenService = authenService;
            _currentAccount = currentAccount;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (!string.IsNullOrEmpty(token))
            {
                var account = await _authenService.GetAuthenticatedAccount(token);
                context.User = new ClaimsPrincipal(new ClaimsIdentity(account, "Bearer"));
                _currentAccount.SetCurrentAccount(context.User);
            }
            await next(context);
        }
    }
}