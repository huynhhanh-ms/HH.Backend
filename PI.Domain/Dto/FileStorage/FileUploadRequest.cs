﻿namespace PI.Domain.Dto.FileStorage
{
    public class FileUploadRequest
    {
        public string Name { get; set; } = default!;
        public string Extension { get; set; } = default!;
        public string Data { get; set; } = default!;
    }
}