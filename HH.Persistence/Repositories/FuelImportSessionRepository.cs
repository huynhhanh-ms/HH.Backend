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

public class FuelImportSessionRepository : GenericRepository<FuelImportSession>, IFuelImportSessionRepository
{
    public FuelImportSessionRepository(DbContext context) : base(context)
    {
    }
}
