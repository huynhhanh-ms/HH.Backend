namespace PI.Persitence.Repository
{
    public class NotificationRepository : GenericRepository<Notification>
    {
        public NotificationRepository(DbContext context) : base(context)
        {
        }

        public override Task<IPagedList<Notification>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            throw new NotImplementedException();
        }

        public override Task<IPagedList<TResult>> SearchAsync<TResult>(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            throw new NotImplementedException();
        }
    }
}