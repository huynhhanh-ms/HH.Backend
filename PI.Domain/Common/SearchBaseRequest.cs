using PI.Domain.Common.PagedLists;

namespace PI.Domain.Common
{
    public class SearchBaseRequest
    {
        public string? KeySearch { get; set; } = null;
        public PagingQuery PagingQuery { get; set; } = new PagingQuery();
        public string? OrderBy { get; set; } = null;
    }
}
