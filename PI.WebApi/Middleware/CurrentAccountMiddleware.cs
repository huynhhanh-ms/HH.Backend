using System.Security.Claims;
using PI.Application.Service.Authen;
using PI.Domain.Infrastructure.Auth;

namespace PI.WebApi.Helpers
{
    public class CurrentAccountMiddleware : IMiddleware
    {
        private readonly IAuthenService _authenService;
        private readonly ICurrentAccount _currentAccount;

        public CurrentAccountMiddleware(IAuthenService authenService, ICurrentAccount currentAccount)
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