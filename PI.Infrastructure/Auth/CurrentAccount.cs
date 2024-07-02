using PI.Domain.Infrastructure.Auth;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace PI.Infrastructure.Auth
{
    public class CurrentAccount : ICurrentAccount
    {
        private ClaimsPrincipal? _user;
        public void SetCurrentAccount(ClaimsPrincipal user)
        {
            if (_user != null)
            {
                throw new Exception("Current account has been set");
            }
            _user = user;
        }

        public int GetAccountId() => IsAuthenticated() ? int.Parse(_user?.FindFirst("nameid")?.Value ?? "0") : 0;

        public string GetAccountName() => IsAuthenticated() ? _user?.FindFirst("unique_name")?.Value ?? "" : "";

        public bool IsAuthenticated() => _user?.Identity?.IsAuthenticated ?? false;

        public IEnumerable<Claim>? GetClaims()
        {
            throw new NotImplementedException();
        }

        public string GetAccountRole()
        {
            return IsAuthenticated() ? _user?.FindFirst(ClaimTypes.Role)?.Value ?? ""
                             : "";
        }

        public string GetFullName()
        {
            return IsAuthenticated() ? _user?.FindFirst("fullname")?.Value ?? ""
                             : "";
        }
    }
}