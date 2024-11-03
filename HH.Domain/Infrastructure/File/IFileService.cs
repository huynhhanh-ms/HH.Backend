using HH.Domain.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HH.Domain.Infrastructure.File
{
    public interface IFileService
    {
        FileResponse GetFileUrl(string storageName);
        Task<string> UploadAsync(string? storagePath, IFormFile file);
        Task<FileResponse> UploadAsync(string storageName, List<IFormFile> files);
        Task DownloadAsync(string? storagePath, string localPath);
        Task DownloadAsync(string[] storageName, string[] localPath);
    }

}
