using Google.Cloud.Storage.V1;
using HH.Domain.Common;
using HH.Domain.Dto;
using HH.Domain.Exceptions;
using HH.Domain.Infrastructure.File;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.Infrastructure.Firebase
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

            var listFile = list.Select(item => item.MediaLink).ToList();

            var listWithExpiry = new List<string>();
            foreach (var item in list)
            {
                UrlSigner urlSigner = UrlSigner.FromServiceAccountPath(AppConfig.FirebaseConfig.DefaultPath);
                string signedUrl = urlSigner.Sign(AppConfig.FirebaseConfig.BucketName, item.Name, TimeSpan.FromHours(1), HttpMethod.Get);
                listWithExpiry.Add(signedUrl);
            }

            return new FileResponse(storageName, listFile);
        }

        //upload file
        public async Task<string> UploadAsync(string? storagePath, IFormFile file)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    var uploadObject = await _storageClient.UploadObjectAsync(
                        bucket: AppConfig.FirebaseConfig.BucketName, 
                        objectName: storagePath ?? file.FileName,
                        contentType: file.ContentType,
                        source: memoryStream

                        //set public read
                        //options: new UploadObjectOptions { PredefinedAcl = PredefinedObjectAcl.PublicRead }
                    );
                    return uploadObject.MediaLink;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetExceptionMessage());
                return string.Empty;
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

        //download file
        public async Task DownloadAsync(string? storagePath, string localPath)
        {
            try
            {
                using (var fileStream = File.OpenWrite(localPath))
                {
                    await _storageClient.DownloadObjectAsync(AppConfig.FirebaseConfig.BucketName, storagePath,
                        fileStream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error downloading file: {ex.Message}");
            }
        }

        public async Task DownloadAsync(string[] storageNames, string[] localPaths)
        {
            var tasks = storageNames.Zip(localPaths, (storageName, localPath) => DownloadAsync(storageName, localPath));
            await Task.WhenAll(tasks);
        }
    }

}
