using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using PI.Domain.Common;
using PI.Domain.Dto.FileStorage;
using PI.Domain.Infrastructure.File;

namespace PI.Infrastructure.Firebase
{
    public class FirebaseFileService : IFileService
    {
        public readonly FirebaseClient _firebaseClient;
        public readonly StorageClient _storageClient;

        public FirebaseFileService()
        {
            _firebaseClient = new FirebaseClient();
            _storageClient = _firebaseClient.Storage;
        }

        //get file url
        public FileResponse GetFileUrl(string storageName)
        {
            //get all file in storage
            var list = _storageClient.ListObjects(AppConfig.FirebaseConfig.BucketName, storageName);
            var listFile = list.Select(item => item.MediaLink.Replace("/dowload/storage", "")
                               .Replace("storage", "firebasestorage")
                               .Replace("v1", "v0"))
                               .ToList();

            return new FileResponse(storageName, listFile);
        }

        //upload file
        public async Task<string> UploadAsync(string? storagePath, IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var uploadObject = await _storageClient.UploadObjectAsync(
                    bucket: AppConfig.FirebaseConfig.BucketName,
                    objectName: storagePath ?? file.FileName,
                    contentType: file.ContentType,
                    source: memoryStream
                );
                return uploadObject.MediaLink
                                   .Replace("/download/storage", "")
                                   .Replace("storage", "firebasestorage")
                                   .Replace("v1", "v0");
            }
        }

        //upload multiple file
        public async Task<FileResponse> UploadAsync(string storageName, List<IFormFile> files)
        {
            var listFile = new List<string>();
            foreach (var file in files)
            {
                var storagePath = $"{storageName}/{file.FileName}";
                var url = await UploadAsync(storagePath, file);
                listFile.Add(url);
            }

            return new FileResponse(storageName, listFile);
        }
    }
}
