using PI.Domain.Models;
using PI.Domain.Repositories.Common;

namespace PI.Domain.Repositories
{
    public interface IIngredientRepository : IGenericRepository<Ingredient>
    {
        Task<Ingredient?> FindByName(string name);
    }
}