using HH.Domain.Models;
using HH.Domain.Repositories;
using HH.Persistence.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.Persistence.Repositories;

public class WeighingHistoryRepository : GenericRepository<WeighingHistory>, IWeighingHistoryRepository 
{
    public WeighingHistoryRepository(DbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<WeighingHistory>> GetsInDateRange(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        return await _dbContext.Set<WeighingHistory>().AsNoTracking()
            .Where(x => x.CreatedAt >= startDate.DateTime && x.CreatedAt <= endDate.DateTime).ToListAsync();
    }
}
