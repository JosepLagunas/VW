using System;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Laklp.Models.Settings;

namespace Laklp.Services.S3Service
{
    internal class S3Service : IS3Service
    {
        private readonly AWSAccessCredentials _awsAccessCredentials;
        private readonly S3Settings _s3Settings;

        public S3Service(AWSAccessCredentials awsAccessCredentials, S3Settings s3Settings)
        {
            _awsAccessCredentials = awsAccessCredentials;
            _s3Settings = s3Settings;
        }

        public string GetS3FileUrl(string bucketName, string key)
        {
            var s3Config = new AmazonS3Config {RegionEndpoint = RegionEndpoint.EUWest1};
            var s3Client = new AmazonS3Client(_awsAccessCredentials.AccessKey,
                _awsAccessCredentials.SecretKey, s3Config);
            
            var request = new GetPreSignedUrlRequest
            {
                BucketName = bucketName,
                Key = key,
                Expires = DateTime.Now.Add(TimeSpan.FromMinutes(_s3Settings.UrlsLifeTime))
            };
            return s3Client.GetPreSignedURL(request);
        }
    }
}