﻿namespace PI.Domain.Common.PagedLists
{
    public interface IPagedList<TEntity> : IList<TEntity>
    {
        int CurrentPage { get; }
        int PageSize { get; }
        int TotalCount { get; }
        int TotalPages { get; }
        bool HasPrevious { get; }
        bool HasNext { get; }
    }
}
