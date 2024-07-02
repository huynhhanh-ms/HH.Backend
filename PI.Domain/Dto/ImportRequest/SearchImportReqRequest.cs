using PI.Domain.Common;
using PI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Domain.Dto.ImportRequest
{
    public class SearchImportReqRequest : SearchBaseRequest
    {
        public ImportRequestStatus? ImportStatus { get; set; } = null;

        public DateTime? CreateDateFrom { get; set; } = null;

        public DateTime? CreateDateTo { get;} = null;
    }
}
