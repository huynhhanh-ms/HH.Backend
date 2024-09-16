using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.Domain.Common
{
    public class PagingResponse<T> where T : class
    {
        public int CurrentPage
        {
            get
            {
                return Data.CurrentPage;
            }
            set
            {
            }
        }
        public int TotalPages
        {
            get
            {
                return Data.TotalPages;
            }
            set { }
        }
        public int PageSize
        {
            get
            {
                return Data.PageSize;
            }
            set { }
        }
        public int TotalCount
        {
            get
            {
                return Data.TotalCount;
            }
            set { }
        }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public IPagedList<T> Data { get; set; } = new PagedList<T>();
    }
}
