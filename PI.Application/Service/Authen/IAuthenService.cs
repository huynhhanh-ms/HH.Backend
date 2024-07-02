using System.Security.Claims;
using PI.Domain.Common;
using PI.Domain.Dto.Account;
using PI.Domain.Dto.Authen;

namespace PI.Application.Service.Authen
{
    public interface IAuthenService
    {
        Task<ApiResponse<AuthenResponse>> Login(LoginRequest request);
        Task<ApiResponse<AuthenResponse>> RefreshToken(string accessToken);
        Task<IEnumerable<Claim>> GetAuthenticatedAccount(string accessToken);
        Task<ApiResponse<AccountResponse>> GetCurrentAccount();
        Task Logout();
    }
}