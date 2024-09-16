using HH.Domain.Infrastructure.Auth;
using System.Reflection.Metadata;
using System.Security.Claims;
using HH.Domain.Models;

namespace HH.Infrastructure.Auth
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
        //set current account follow claims and account
        public void SetAccount(Account account)
        {
            var claims = new List<Claim>
            {
                new Claim("nameid", account.AccountId.ToString()),
                new Claim("unique_name", account.Username),
                new Claim("fullname", account.Fullname),
                new Claim("role", account.Role)
            };
            var identity = new ClaimsIdentity(claims, "token");
            _user = new ClaimsPrincipal(identity);
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
            return IsAuthenticated() ? _user?.FindFirst("role")?.Value ?? ""
                             : "";
        }

        public string GetFullName()
        {
            return IsAuthenticated() ? _user?.FindFirst("fullname")?.Value ?? ""
                             : "";
        }

        public ClaimsPrincipal Clone() => _user?.Clone() ?? new ClaimsPrincipal();
    }
}