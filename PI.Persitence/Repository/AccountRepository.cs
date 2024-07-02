using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using PI.Domain.Common.PagedLists;
using PI.Domain.Dto.Account;
using PI.Domain.Models;
using PI.Persitence.Repository.Common;
using PI.Persitence.Repository.Helper;

namespace PI.Persitence.Repository
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public AccountRepository(DbContext context) : base(context)
        {
        }

        public override Task<IPagedList<Account>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            return _dbSet.AsNoTracking()
                .WhereWithExist(p => string.IsNullOrEmpty(keySearch) ||
                                     p.Fullname.Contains(keySearch))
                .Where(p => p.Role == AccountRole.Staff.ToString())
                .WithOrderByString(orderBy)
                .ToPagedListAsync(pagingQuery);
        }

        public override async Task<IPagedList<TResult>> SearchAsync<TResult>(string keySearch, PagingQuery pagingQuery,
            string orderBy)
        {
            return await _dbSet.AsNoTracking()
                .WhereWithExist(p => string.IsNullOrEmpty(keySearch) ||
                                     p.Fullname.Contains(keySearch))
                .Where(p => p.Role == AccountRole.Staff.ToString())
                .WithOrderByString(orderBy)
                .SelectWithField<Account, TResult>()
                .ToPagedListAsync(pagingQuery);
        }

        public Task<IPagedList<AccountResponse>> SearchAccountAsync(string keySearch, string? role, bool? isFree,
            PagingQuery pagingQuery, string orderBy)
        {
            return _dbSet.AsNoTracking()
                .IncludeIf(true, x => x.StockCheckStaffs)
                .WhereWithExist(p => string.IsNullOrEmpty(keySearch) ||
                                     p.Fullname.Contains(keySearch))
                .Where(p => role == null || p.Role == role.ToUpper())
                .WithOrderByString(orderBy)
                .SelectWithField<Account, AccountResponse>()
                .Where(p => isFree == null || p.IsFree == isFree)
                .ToPagedListAsync(pagingQuery);
        }

        public Task<List<int>> GetAccountIdsByRoleAsync(string role)
        {
            return _dbSet.AsNoTracking()
                .Where(p => p.Role == role)
                .Select(p => p.AccountId)
                .ToListAsync();
        }
    }
}