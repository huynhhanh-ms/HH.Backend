using HH.Domain.Dto;
using HH.Domain.Models;
using HH.Domain.Repositories;
using HH.Persistence.Repositories.Common;
using HH.Persistence.Repositories.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.Persistence.Repositories;

public class ExpenseTypeRepository : GenericRepository<ExpenseType>, IExpenseTypeRepository
{
    public ExpenseTypeRepository(DbContext context) : base(context)
    {
    }
    public override async Task<ExpenseType> FindAsync(int entityId)
    {
        return await _dbSet.AsNoTracking().FirstAsync(x => x.Id == entityId).ConfigureAwait(false);
    }

}
