using Microsoft.AspNetCore.Http;
using PI.Domain.Dto.FileStorage;

namespace PI.Domain.Infrastructure.File
{
    public interface IFileService
    {
        FileResponse GetFileUrl(string storageName);
        Task<string> UploadAsync(string? storagePath, IFormFile file);
        Task<FileResponse> UploadAsync(string storageName, List<IFormFile> files);
    }
}