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

public class PetrolPumpRepository : GenericRepository<PetrolPump>, IPetrolPumpRepository
{
    public PetrolPumpRepository(DbContext context) : base(context)
    {
    }
    //public override async Task<PetrolPump?> FindAsync(int entityId)
    //{
    //    return await _dbSet.AsNoTracking()
    //                        .WhereStringWithExist(string.Empty)
    //                        .Include(x => x.PetrolPumps)
    //                        .ThenInclude(x => x.Tank)
    //                        .Include(x => x.Expenses.Where(x => !x.IsDeleted))
    //                        .ThenInclude(x => x.ExpenseType)
    //                        //.SelectWithField<PetrolPump, PetrolPumpGetDto>()
    //                        .FirstOrDefaultAsync(x => x.Id == entityId)
    //                        .ConfigureAwait(false);
    //}

}
