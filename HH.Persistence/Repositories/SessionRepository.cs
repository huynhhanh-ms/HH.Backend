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

public class SessionRepository : GenericRepository<Session>, ISessionRepository
{
    public SessionRepository(DbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<SessionGetDto>> FindAll()
    {
        return await _dbSet.AsNoTracking()
                            .Include(x => x.PetrolPumps)
                            .ThenInclude(x => x.Tank)
                            .Include(x => x.Expenses.Where(x => !x.IsDeleted))
                            .ThenInclude(x => x.ExpenseType)
            .SelectWithField<Session, SessionGetDto>()
            .ToListAsync();
    }

    public async Task<Session?> FindAsync(int entityId)
    {
        return await _dbSet.AsNoTracking()
                            .WhereStringWithExist(string.Empty)
                            .Include(x => x.PetrolPumps)
                            .ThenInclude(x => x.Tank)
                            .Include(x => x.Expenses.Where(x => !x.IsDeleted))
                            //.ThenInclude(x => x.ExpenseType)
                            //.SelectWithField<Session, SessionGetDto>()
                            .FirstOrDefaultAsync(x => x.Id == entityId)
                            .ConfigureAwait(false);
    }

    public async Task<Session?> FindSingleAsync(int entityId)
    {
        return await _dbSet.AsNoTracking()
                            .WhereStringWithExist(string.Empty)
                            .Include(x => x.PetrolPumps)
                            //.ThenInclude(x => x.Tank)
                            .Include(x => x.Expenses.Where(x => !x.IsDeleted))
                            //.ThenInclude(x => x.ExpenseType)
                            //.SelectWithField<Session, SessionGetDto>()
                            .FirstOrDefaultAsync(x => x.Id == entityId)
                            .ConfigureAwait(false);
    }




}
