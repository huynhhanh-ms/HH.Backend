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
}
