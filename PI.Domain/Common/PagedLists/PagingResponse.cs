namespace PI.Domain.Common.PagedLists
{
    public class PagingResponse<T> where T : class
    {
        public int CurrentPage
        {
            get
            {
                return Data.CurrentPage;
            }
            set
            {
            }
        }
        public int TotalPages
        {
            get
            {
                return Data.TotalPages;
            }
            set { }
        }
        public int PageSize
        {
            get
            {
                return Data.PageSize;
            }
            set { }
        }
        public int TotalCount
        {
            get
            {
                return Data.TotalCount;
            }
            set { }
        }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public IPagedList<T> Data { get; set; } = new PagedList<T>();
    }
}