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
    //public virtual async Task<IEnumerable<Session>> GetAllAsync()
    //{
    //    return await _dbSet.AsNoTracking()
    //                        .WhereStringWithExist(string.Empty)
    //                        .Include(x => x.Tank)
    //                        //.SelectWithField<Session, SessionGetDto>()
    //                        .WithOrderByString("ImportDate:desc")
    //                        .ToListAsync()
    //                        .ConfigureAwait(false);
    //}

}
