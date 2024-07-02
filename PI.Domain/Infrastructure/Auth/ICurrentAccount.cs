using System.Security.Claims;

namespace PI.Domain.Infrastructure.Auth
{
    public interface ICurrentAccount
    {
        void SetCurrentAccount(ClaimsPrincipal user);
        int GetAccountId();
        string GetAccountName();
        string GetFullName();
        string GetAccountRole();    
        bool IsAuthenticated();
        IEnumerable<Claim>? GetClaims();
    }
}