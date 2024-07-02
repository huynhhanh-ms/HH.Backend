using PI.Domain.Common.PagedLists;
using PI.Domain.Dto.Account;
using PI.Domain.Infrastructure.Auth;
using PI.Domain.Models;
using PI.Domain.Repositories;
using PI.Domain.Repositories.Common;

namespace PI.Application.Service
{
    public class AccountService : BaseService, IAccountService
    {
        private readonly IPasswordHasher _passwordHasher;

        public AccountService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher) : base(unitOfWork)
        {
            _passwordHasher = passwordHasher;
        }

        public async Task<ApiResponse<string>> CreateStaffAccount(RegisterAccountRequest request)
        {
            //check if username is existed
            var account = await _unitOfWork.Resolve<Account>().FindAsync(x => x.Username.Equals(request.Username));
            ValidateException.ThrowIf(account != null, "Username is existed in system already!");

            account = request.Adapt<Account>();
            account.PasswordHash = _passwordHasher.Hash(request.Password);
            account.Role = AccountRole.Staff.ToString();

            await _unitOfWork.Resolve<Account>().CreateAsync(account);
            await _unitOfWork.SaveChangesAsync();

            return Success<string>("Create account successfully!");
        }

        public async Task<ApiResponse<AccountResponse>> GetInformationStaff(string username)
        {
            var account = await _unitOfWork.Resolve<Account>().FindAsync(x => x.Username.Equals(username)
                                                                              && x.Role.Equals(AccountRole.Staff
                                                                                  .ToString()));
            ValidateException.ThrowIfNull(account, "Account is not existed");

            var response = account.Adapt<AccountResponse>();

            return Success(response);
        }

        public async Task<PagingApiResponse<AccountResponse>> SearchAccountStaff(SearchStaffReq request)
        {
            try
            {
                var response = await _unitOfWork.Resolve<IAccountRepository>()
                    .SearchAccountAsync(request.KeySearch, AccountRole.Staff.ToString(), request.IsFree, request.PagingQuery,
                        request.OrderBy);
                return Success(response);
            }
            catch (Exception ex)
            {
                return PagingFailed<AccountResponse>(ex.GetExceptionMessage());
            }
        }

        //get account from role
        public async Task<PagingApiResponse<AccountResponse>> SearchAccountByRole(SearchAccountReq request)
        {
            try
            {
                var response = await _unitOfWork.Resolve<IAccountRepository>()
                    .SearchAccountAsync(request.KeySearch, request.Role, request.IsFree, request.PagingQuery,
                        request.OrderBy);
                return Success(response);
            }
            catch (Exception ex)
            {
                return PagingFailed<AccountResponse>(ex.GetExceptionMessage());
            }
        }


        //update account
        public async Task<ApiResponse<string>> UpdateAccountInfo(int accountId, UpdateAccountRequest request)
        {
            try
            {
                var account = await _unitOfWork.Resolve<Account>()
                    .FindAsync(x => x.AccountId == accountId
                                    && x.Role == AccountRole.Staff.ToString());

                if (account == null)
                    throw new ArgumentException("Account is not existed");
                account.Fullname = request.Fullname;
                account.Email = request.Email;
                account.Phone = request.Phone;
                await _unitOfWork.Resolve<Account>().UpdateAsync(account);
                await _unitOfWork.SaveChangesAsync();
                return Success<string>("Update account successfully!");
            }
            catch (Exception ex)
            {
                return Failed<string>(ex.GetExceptionMessage());
            }
        }

        public async Task<ApiResponse<string>> UpdateStatusAccount(int accountId, UpdateAccountStatusRequest request)
        {
            try
            {
                var account = await _unitOfWork.Resolve<Account>()
                    .FindAsync(x => x.AccountId == accountId && x.Role == AccountRole.Staff.ToString());
                if (account == null)
                    throw new ArgumentException("Account is not existed");
                account.Status = request.Status.ToString().ToLower();
                await _unitOfWork.Resolve<Account>().UpdateAsync(account);
                await _unitOfWork.SaveChangesAsync();
                return Success<string>("Update status account successfully!");
            }
            catch (Exception ex)
            {
                return Failed<string>(ex.GetExceptionMessage());
            }
        }

        //del
        public async Task<ApiResponse<string>> Delete(int accountId)
        {
            try
            {
                var account = await _unitOfWork.Resolve<Account>()
                    .FindAsync(x => x.AccountId == accountId && x.Role == AccountRole.Staff.ToString());
                if (account == null)
                    throw new ArgumentException("Account is not existed");
                await _unitOfWork.Resolve<Account>().DeleteAsync(accountId);
                await _unitOfWork.SaveChangesAsync();
                return Success<string>("Delete account successfully!");
            }
            catch (Exception ex)
            {
                return Failed<string>(ex.GetExceptionMessage());
            }
        }
    }
}