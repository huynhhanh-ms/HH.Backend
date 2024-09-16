using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.Domain.Common
{
    public class PagedList<TEntity> : List<TEntity>, IPagedList<TEntity> where TEntity : class
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 0;
        public int TotalCount { get; set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PagedList()
        {
        }

        public PagedList(List<TEntity> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            CurrentPage = pageNumber;
            PageSize = pageSize;

            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);
        }

        /// <summary>
        /// Excute query with pagination
        /// </summary>
        /// <param name="queryList"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task LoadData(IQueryable<TEntity> queryList, int pageNumber, int pageSize)
        {

            ValidateException.ThrowIf(pageNumber <= 0 || pageSize <= 0,
                "Page number or page size must be greater than 0");

            var items = await queryList
                                .AsNoTracking()
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync()
                                .ConfigureAwait(false);

            var totalCount = await queryList.CountAsync();

            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            AddRange(items);
        }

        public async Task LoadDataAsync(IQueryable<TEntity> queryList, PagingQuery paginationParams)
        {
            ValidateException.ThrowIfNull(paginationParams,
                "Paging Params is required");

            ValidateException.ThrowIf(paginationParams.PageSize <= 0 || paginationParams.PageNumber <= 0,
               "Page number or page size must be greater than 0");

            await LoadData(queryList, paginationParams.PageNumber, paginationParams.PageSize);
        }
    }
}
