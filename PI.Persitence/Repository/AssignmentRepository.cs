

using PI.Domain.Dto.Assignment;
using PI.Domain.Extensions;

namespace PI.Persitence.Repository
{
    internal class AssignmentRepository : GenericRepository<Assignment>, IAssignmentRepository
    {
        public AssignmentRepository(DbContext context) : base(context)
        {
        }

        public override Task<Assignment?> FindAsync(int entityId)
        {
            return _dbSet
                .Include(x => x.Assignee)
                .Include(x => x.Reporter)
                .FirstOrDefaultAsync(x => x.AssignmentId == entityId && x.IsDeleted == false);
        }

        public override Task<IPagedList<Assignment>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            throw new NotImplementedException();
        }

        public override Task<IPagedList<TResult>> SearchAsync<TResult>(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedList<AssignmentResponse>> SearchMyAssignment(SearchMyAssignmentRequest request)
        {
            return _dbSet
                .Include(x => x.Assignee)
                .Include(x => x.Reporter)
                .WhereWithExist(x =>
                    (string.IsNullOrEmpty(request.KeySearch) || x.Title.Contains(request.KeySearch))
                    && (
                        request.Role == null
                        || (request.Role == AssignmentRole.Reporter && x.ReporterId == request.AccountId)
                        || (request.Role == AssignmentRole.Assignee && x.AssigneeId == request.AccountId)
                    )
                    && (request.Status == null || x.Status == request.Status.ToEnumString())
                    && (string.IsNullOrEmpty(request.Label) || x.Label.Contains(request.Label))
                 )
                .SelectWithField<Assignment, AssignmentResponse>()
                .ToPagedListAsync(request.PagingQuery);
        }
    }
}
