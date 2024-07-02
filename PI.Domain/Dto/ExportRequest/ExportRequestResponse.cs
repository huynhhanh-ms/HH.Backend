using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Domain.Dto.ExportRequest
{
    public class ExportRequestResponse
    {
        public int ExportRequestId { get; set; }

        public string? ExportStatus { get; set; }

        public DateTime CreatedAt { get; set; }

        public int CreatedBy { get; set; }

        public IEnumerable<int> Shipments { get; set; } = new List<int>();

        public IEnumerable<string> ProductNames { get; set; } = new List<string>(); // ExportRequsestDetail -> ProductUnit -> Product -> Name
    }

   
}
