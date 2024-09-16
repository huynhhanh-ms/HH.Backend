using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.Domain.Common.Entity
{
    public interface IDeletable
    {
        bool IsDeleted { get; set; }
    }
}
