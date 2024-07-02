using PI.Domain.Common.PagedLists;
using PI.Domain.Dto.Account;

namespace PI.Application.Service
{
    public interface IAccountService
    {
        Task<ApiResponse<string>> CreateStaffAccount(RegisterAccountRequest request);
        Task<ApiResponse<AccountResponse>> GetInformationStaff(string username);

        Task<PagingApiResponse<AccountResponse>> SearchAccountStaff(SearchStaffReq request);

        Task<PagingApiResponse<AccountResponse>> SearchAccountByRole(SearchAccountReq request);
        Task<ApiResponse<string>> UpdateStatusAccount(int accountId, UpdateAccountStatusRequest request);
        Task<ApiResponse<string>> UpdateAccountInfo(int accountId, UpdateAccountRequest request);
        Task<ApiResponse<string>> Delete(int accountId);
    }
}