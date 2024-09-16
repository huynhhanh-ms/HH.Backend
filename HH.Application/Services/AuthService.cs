using HH.Application.Common;
using HH.Domain.Common;
using HH.Domain.Exceptions;
using HH.Domain.Dto.Account;
using HH.Domain.Dto.Authen;
using HH.Domain.Enums;
using HH.Domain.Infrastructure.Auth;
using HH.Domain.Models;
using HH.Domain.Repositories.Common;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HH.Application.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;
        //private readonly ICacheService _cacheService;
        private readonly ICurrentAccount _currentAccount;

        public AuthService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, ITokenService tokenService,
            //ICacheService cacheService, 
            ICurrentAccount currentAccount) :
            base(unitOfWork)
        {
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            //_cacheService = cacheService;
            _currentAccount = currentAccount;
        }

        public async Task<ApiResponse<AuthResponse>> Login(LoginRequest request)
        {
            var account = await _unitOfWork.Resolve<Account>().FindAsync(x => x.Username.Equals(request.Username));

            // Check if username is existed
            ValidateException.ThrowIfNull(account, "Username is not existed");
            // Check if password is correct
            ValidateException.ThrowIf(!account.PasswordHash.Equals(request.Password), "Password is incorrect");
            //ValidateException.ThrowIf(!_passwordHasher.Verify(account.PasswordHash, request.Password), "Password is incorrect");

            // Check if account is locked
            ValidateException.ThrowIf(account.Status == AccountStatus.Inactive.ToString(), "Account is locked");

            //create token
            var accessToken = _tokenService.Encode(new GenerateTokenRequest
            {
                Id = account.AccountId.ToString(),
                Username = account.Username,
                Fullname = account.Fullname,
                Role = Enum.Parse<AccountRole>(account.Role),
                ExpireHours = AppConfig.JwtSetting.AccessTokenExpiration
            });
            var refreshToken = _tokenService.Encode(new GenerateTokenRequest
            {
                Id = account.AccountId.ToString(),
                Username = account.Username,
                Fullname = account.Fullname,
                Role = Enum.Parse<AccountRole>(account.Role),
                ExpireHours = AppConfig.JwtSetting.RefreshTokenExpiration
            });

            //set refresh token in cache
            await SetRefreshTokenInCache(refreshToken, account.AccountId.ToString());

            return Success(new AuthResponse
            {
                AccessToken = accessToken,
                ExpirationTime = DateTime.UtcNow.AddHours(AppConfig.JwtSetting.AccessTokenExpiration)
            });
        }

        public async Task<ApiResponse<AuthResponse>> RefreshToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
                throw new UnauthorizedException("Access token is required");
            //check access token
            var claims = _tokenService.DecodeWithoutExpiration(accessToken);
            //get expire date
            var expireDate = claims.FirstOrDefault(x => x.Type.Equals("exp"))?.Value;
            //check if access token is expired
            if (DateTimeOffset.FromUnixTimeSeconds(long.Parse(expireDate)).UtcDateTime > DateTime.UtcNow)
                throw new UnauthorizedException("Access token is not expired");
            var accountId = claims.FirstOrDefault(x => x.Type.Equals("nameid"))?.Value;
            if (accountId == null)
                throw new UnauthorizedException("Access token is invalid");
            //get account
            var account = await _unitOfWork.Resolve<Account>().FindAsync(int.Parse(accountId));
            if (account == null)
                throw new UnauthorizedException("Account is not existed");
            //get refresh token from cache
            //var key = _cacheService.GetKeyName(KeyType.RefreshToken, accountId);
            //var refreshToken = await _cacheService.GetAsync<string>(key);
            //if (refreshToken == null)
            //    throw new UnauthorizedException("Refresh token is not existed");
            //create new token
            var newAccessToken = _tokenService.Encode(new GenerateTokenRequest
            {
                Id = account.AccountId.ToString(),
                Username = account.Username,
                Fullname = account.Fullname,
                Role = Enum.Parse<AccountRole>(account.Role),
                ExpireHours = AppConfig.JwtSetting.AccessTokenExpiration
            });
            var newRefreshToken = _tokenService.Encode(new GenerateTokenRequest
            {
                Id = account.AccountId.ToString(),
                Username = account.Username,
                Fullname = account.Fullname,
                Role = Enum.Parse<AccountRole>(account.Role),
                ExpireHours = AppConfig.JwtSetting.RefreshTokenExpiration
            });
            //set refresh token in cache
            await SetRefreshTokenInCache(newRefreshToken, account.AccountId.ToString());
            return Success(new AuthResponse
            {
                AccessToken = newAccessToken,
                ExpirationTime = DateTime.UtcNow.AddHours(AppConfig.JwtSetting.AccessTokenExpiration)
            });
        }

        public async Task<IEnumerable<Claim>> GetAuthenticatedAccount(string accessToken)
        {
            var claims = _tokenService.Decode(accessToken);
            if (claims == null)
                throw new UnauthorizedException("Access token is invalid");
            var accountId = claims.FirstOrDefault(x => x.Type.Equals("nameid"))?.Value;
            if (accountId == null)
                throw new UnauthorizedException("Invalid or missing account id in the access token");
            var account = await _unitOfWork.Resolve<Account>().FindAsync(int.Parse(accountId));
            if (account == null)
                throw new UnauthorizedException("Account is not existed");
            return claims;
        }

        public async Task Logout()
        {
            var accountId = _currentAccount.GetAccountId();
            //remove refresh token from cache
            //var key = _cacheService.GetKeyName(KeyType.RefreshToken, accountId.ToString());
            //await _cacheService.RemoveAsync(key);
        }

        public async Task<ApiResponse<AccountRes>> GetCurrentAccount()
        {
            try
            {
                var accountId = _currentAccount.GetAccountId();
                var account = await _unitOfWork.Resolve<Account>().FindAsync(accountId);
                if (account == null)
                    throw new ArgumentException("Account is not existed");
                return Success(account.Adapt<AccountRes>());
            }
            catch (Exception ex)
            {
                return Failed<AccountRes>(ex.GetExceptionMessage(), HttpStatusCode.Unauthorized);
            }
        }

        //set token in cache
        private async Task SetRefreshTokenInCache(string token, string accountId)
        {
            //var key = _cacheService.GetKeyName(KeyType.RefreshToken, accountId);
            //await _cacheService.SetAsync(key, token, TimeSpan.FromHours(AppConfig.JwtSetting.RefreshTokenExpiration));
        }

    }
}
