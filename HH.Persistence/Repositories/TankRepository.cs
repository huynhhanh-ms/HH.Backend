using HH.Domain.Models;
using HH.Domain.Repositories;
using HH.Persistence.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.Persistence.Repositories;

public class TankRepository : GenericRepository<Tank>, ITankRepository 
{
    public TankRepository(DbContext context) : base(context)
    {
    }
}
