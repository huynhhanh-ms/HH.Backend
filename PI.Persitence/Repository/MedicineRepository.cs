using Microsoft.EntityFrameworkCore;
using PI.Domain.Common.PagedLists;
using PI.Domain.Models;
using PI.Domain.Repositories;
using PI.Persitence.Repository.Common;
using PI.Persitence.Repository.Helper;

namespace PI.Persitence.Repository
{
    public class MedicineRepository : GenericRepository<Medicine>, IMedicineRepository
    {
        public MedicineRepository(DbContext context) : base(context)
        {
        }

        public override Task<IPagedList<Medicine>> SearchAsync(string keySearch, PagingQuery pagingQuery,
            string orderBy)
        {
            return _dbSet.AsNoTracking()
                .WhereWithExist(p => string.IsNullOrEmpty(keySearch) ||
                                      p.Name.Contains(keySearch))
                .WithOrderByString(orderBy)
                .ToPagedListAsync(pagingQuery);
        }

        public override Task<IPagedList<TResult>> SearchAsync<TResult>(string keySearch, PagingQuery pagingQuery,
            string orderBy)
        {
            return _dbSet.AsNoTracking()
                .WhereWithExist(p => string.IsNullOrEmpty(keySearch) ||
                                      p.Name.Contains(keySearch))
                .WithOrderByString(orderBy)
                .SelectWithField<Medicine, TResult>()
                .ToPagedListAsync(pagingQuery);
        }

        public Task<Medicine?> FindByRegistrationNo(string registrationNo)
        {
            return _dbSet.AsNoTracking()
                .Include(p => p.Ingredients)
                .FirstOrDefaultAsync(p => p.RegistrationNo == registrationNo);
        }
    }
}