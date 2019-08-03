namespace Laklp.Services.S3Service
{
    internal interface IS3Service
    {
        string GetS3FileUrl(string bucketName, string key);
    }
}