using PI.Domain.Common;
using PI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PI.Domain.Dto.ExportRequest
{
    public class SearchExportReqRequest : SearchBaseRequest
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ExportRequestStatus? ExportStatus { get; set; } = null;
    }
}
