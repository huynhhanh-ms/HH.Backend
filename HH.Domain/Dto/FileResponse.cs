using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.Domain.Dto
{
    public class FileResponse
    {
        public FileResponse(string storageName, List<string> urls)
        {
            StorageName = storageName;
            Urls = urls;
        }
        public string StorageName { get; set; }
        public List<string> Urls { get; set; }
    }

}
