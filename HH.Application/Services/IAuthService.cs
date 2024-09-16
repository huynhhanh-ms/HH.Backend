using HH.Domain.Common;
using HH.Domain.Dto.Account;
using HH.Domain.Dto.Authen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HH.Application.Services
{
    public interface IAuthService
    {
        Task<ApiResponse<AuthResponse>> Login(LoginRequest request);
        Task<ApiResponse<AuthResponse>> RefreshToken(string accessToken);
        Task<IEnumerable<Claim>> GetAuthenticatedAccount(string accessToken);
        Task<ApiResponse<AccountRes>> GetCurrentAccount();
        Task Logout();

    }
}
