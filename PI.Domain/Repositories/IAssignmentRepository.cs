using PI.Domain.Common.PagedLists;
using PI.Domain.Dto.Assignment;
using PI.Domain.Models;
using PI.Domain.Repositories.Common;

namespace PI.Domain.Repositories
{
    public interface IAssignmentRepository : IGenericRepository<Assignment>
    {
        Task<IPagedList<AssignmentResponse>> SearchMyAssignment(SearchMyAssignmentRequest request);
    }
}
