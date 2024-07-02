namespace PI.Domain.Dto.FileStorage
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