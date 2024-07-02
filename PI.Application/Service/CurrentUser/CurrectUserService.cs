using System.Security.Claims;

namespace PI.Application.Service
{
    public class CurrectAccService
    {
        private ClaimsPrincipal? _account;

        public string? Name => _account?.Identity?.Name;


        public bool IsAuthenticated => _account?.Identity?.IsAuthenticated ?? false;

        public int GetAccountId() => int.Parse(_account?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

        public void SetAccount(ClaimsPrincipal account)
        {
            _account = account;
        }

    }
}