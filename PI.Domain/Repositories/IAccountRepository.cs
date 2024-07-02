using PI.Domain.Common.PagedLists;
using PI.Domain.Dto.Account;
using PI.Domain.Dto.Shipment;
using PI.Domain.Enums;
using PI.Domain.Models;
using PI.Domain.Repositories.Common;

namespace PI.Domain.Repositories
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        Task<IPagedList<AccountResponse>> SearchAccountAsync(string keySearch, string? role, bool? isFree, PagingQuery pagingQuery, string orderBy);
        Task<List<int>> GetAccountIdsByRoleAsync(string role);
    }
}