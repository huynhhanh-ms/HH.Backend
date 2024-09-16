using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.Domain.Common
{
    public class PagingQuery
    {
        const int maxPageSize = 10000;
        private int _pageSize = 0;

        public int PageNumber { get; set; } = 1;

        [Range(1, maxPageSize)]
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value > maxPageSize ? maxPageSize : value;
            }
        }

        public PagingQuery()
        {
            PageNumber = 1;
            PageSize = 10;
        }

        public PagingQuery(int? pageNumer, int? pageSize)
        {
            PageNumber = pageNumer ?? 1;
            PageSize = pageSize ?? 10;
        }
    }
}
