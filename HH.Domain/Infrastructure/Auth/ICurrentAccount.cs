using System.Security.Claims;
using HH.Domain.Models;

namespace HH.Domain.Infrastructure.Auth
{
    public interface ICurrentAccount
    {
        void SetCurrentAccount(ClaimsPrincipal user);
        void SetAccount(Account account);
        int GetAccountId();
        string GetAccountName();
        string GetFullName();
        string GetAccountRole();
        bool IsAuthenticated();
        IEnumerable<Claim>? GetClaims();
        ClaimsPrincipal Clone();
    }
}