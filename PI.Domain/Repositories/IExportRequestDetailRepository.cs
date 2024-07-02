using PI.Domain.Dto.ExportRequest;
using PI.Domain.Models;
using PI.Domain.Repositories.Common;

namespace PI.Domain.Repositories
{
    public interface IExportRequestDetailRepository : IGenericRepository<ExportRequestDetail>
    {
        Task<IEnumerable<ExportRequestDetail>> GetByExportRequestId(int exportRequestId);
    }
}
