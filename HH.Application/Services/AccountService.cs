using HH.Application.Common;
using HH.Domain.Common;
using HH.Domain.Dto;
using HH.Domain.Enums;
using HH.Domain.Infrastructure.Auth;
using HH.Domain.Models;
using HH.Domain.Repositories.Common;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace HH.Application.Services
{
    public class AccountService : BaseService, IAccountService
    {
        private readonly IPasswordHasher _passwordHasher;
        //private readonly ICurrentAccount _currentAccount;
        //private readonly ICacheService _cacheService;


        public AccountService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher) : base(unitOfWork)
        {
            _passwordHasher = passwordHasher;
            //_currentAccount = currentAccount;
            //_cacheService = cacheService;
        }

        public async Task<ApiResponse<bool>> Create(AccountCreateDto request)
        {
            //check if username is existed
            var account = await _unitOfWork.Resolve<Account>().FindAsync(x => x.Username.Equals(request.Username));
            ValidateException.ThrowIf(account != null, "Tài khoản đã tồn tại");

            account = request.Adapt<Account>();
            account.PasswordHash = _passwordHasher.Hash(request.Password);
            //account.PasswordHash = request.Password;
            account.Role = AccountRole.Staff.ToString();
            account.Status = AccountStatus.Active.ToString();

            await _unitOfWork.Resolve<Account>().CreateAsync(account);
            await _unitOfWork.SaveChangesAsync();

            return Success<bool>("Tạo tài khoản thành công");
        }

        public Task<ApiResponse<bool>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<Account>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<List<Account>>> Gets(SearchBaseRequest request)
        {
            var accounts = await _unitOfWork.Resolve<Account>().GetAllAsync();
            var response = accounts.Adapt<List<Account>>();
            return Success(response);
        }

        public Task<ApiResponse<bool>> Update(Account request)
        {
            throw new NotImplementedException();
        }
    }
}
