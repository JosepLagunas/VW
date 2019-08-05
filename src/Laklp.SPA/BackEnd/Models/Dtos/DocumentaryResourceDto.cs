using System;

namespace Laklp.Models.Dtos
{
    public class DocumentaryResourceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Properties { get; set; }
        public string S3Key { get; set; }
        public string S3Bucket { get; set; }
        public string S3Path { get; set; }
        public string S3Region { get; set; }
        public string S3Url { get; set; }
        public bool IsEnabled { get; set; }
    }
}