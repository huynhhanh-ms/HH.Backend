using PI.Domain.Models;
using PI.Domain.Repositories.Common;

namespace PI.Domain.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category?> FindById(int id);
    }
}