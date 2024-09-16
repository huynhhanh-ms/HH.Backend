using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.Domain.Common
{
    public class SearchBaseRequest
    {
        public string? KeySearch { get; set; } = null;
        public PagingQuery PagingQuery { get; set; } = new PagingQuery();
        public string? OrderBy { get; set; } = null;
    }
}
