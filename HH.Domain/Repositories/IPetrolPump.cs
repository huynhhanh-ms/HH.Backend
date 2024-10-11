using HH.Domain.Dto;
using HH.Domain.Models;
using HH.Domain.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.Domain.Repositories;

public interface IPetrolPumpRepository : IGenericRepository<PetrolPump>
{
    public new Task<PetrolPump> FindAsync(int entityId);
}

