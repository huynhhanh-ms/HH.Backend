using HH.Application.Common;
using HH.Domain.Common;
using HH.Domain.Enums;
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
        //private readonly IPasswordHasher _passwordHasher;
        //private readonly ICurrentAccount _currentAccount;
        //private readonly ICacheService _cacheService;


        public AccountService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            //_passwordHasher = passwordHasher;
            //_currentAccount = currentAccount;
            //_cacheService = cacheService;
        }

        public async Task<ApiResponse<string>> CreateAccount(Account request)
        {
            //check if username is existed
            var account = await _unitOfWork.Resolve<Account>().FindAsync(x => x.Username.Equals(request.Username));
            ValidateException.ThrowIf(account != null, "Username is existed in system already!");

            account = request.Adapt<Account>();
            //account.PasswordHash = _passwordHasher.Hash(request.Password);
            account.PasswordHash = request.PasswordHash;
            account.Role = AccountRole.Staff.ToString();

            await _unitOfWork.Resolve<Account>().CreateAsync(account);
            await _unitOfWork.SaveChangesAsync();

            return Success<string>("Create account successfully!");
        }

        public async Task<ApiResponse<List<Account>>> Search(SearchBaseRequest request)
        {
            var accounts = await _unitOfWork.Resolve<Account>().GetAllAsync();

            var response = accounts.Adapt<List<Account>>();

            return Success(response);
        }
    }
}
