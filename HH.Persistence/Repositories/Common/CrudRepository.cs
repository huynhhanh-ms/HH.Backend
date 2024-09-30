using HH.Domain.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.Persistence.Repositories.Common;

public class CrudRepository<TEntity> : GenericRepository<TEntity> where TEntity : class, IEntityBase
{
    public CrudRepository(DbContext context) : base(context)
    {

    }
}
