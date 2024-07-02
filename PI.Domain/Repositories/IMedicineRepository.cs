using PI.Domain.Models;
using PI.Domain.Repositories.Common;

namespace PI.Domain.Repositories
{
    public interface IMedicineRepository : IGenericRepository<Medicine>
    {
        Task<Medicine?> FindByRegistrationNo(string registrationNo);
    }
}