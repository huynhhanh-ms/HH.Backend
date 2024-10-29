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

public class FuelImportRepository : GenericRepository<FuelImport>, IFuelImportRepository
{
    public FuelImportRepository(DbContext context) : base(context)
    {
    }
    public async Task<IEnumerable<FuelImport>> GetAllWithInclude()
    {
        return await _dbSet.AsNoTracking()
                            .WhereStringWithExist(string.Empty)
                            .Include(x => x.Tank)
                            //.SelectWithField<FuelImport, FuelImportGetDto>()
                            .WithOrderByString("ImportDate:desc")
                            .ToListAsync()
                            .ConfigureAwait(false);
    }

}
