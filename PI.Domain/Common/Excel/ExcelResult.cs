using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Domain.Common.Excel
{
    public class ExcelResult<T>
    {
        public T Data { get; set; } = default;

        public bool Status { get; set; }

        public string? InnerErrorMessage { get; set; }

        public string? ErrorMessage { get; set; }

        public string? Code { get; set; }
    }
}
