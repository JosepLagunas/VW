using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VWP.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering",
            "Scorching"
        };

        [Authorize]
        [HttpGet("[action]")]
        public async Task<IEnumerable<WeatherForecast>> WeatherForecasts()
        {
            await ConnectToAwsCognitoAsync();
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        private async Task ConnectToAwsCognitoAsync()
        {
            var accessToken = await User.GetAccess_TokenAsync(HttpContext);
            var idToken = await User.GetId_TokenAsync(HttpContext);
            var accessTokenExpiresAt = await User.GetTokenExpirationAsync(HttpContext);

            var credentials = new CognitoAWSCredentials("947115102978",
                "eu-west-1:36610218-b0b9-4906-ab23-00f6f1144103",
                "arn:aws:iam::947115102978:role/Cognito_Auth0_laklpAuth_Role",
                "arn:aws:iam::947115102978:role/Cognito_Auth0_laklpUnauth_Role",
                RegionEndpoint.EUWest1
            );

            //credentials.AddLogin("dev-zqmqcd7w.eu.auth0.com", idToken);

            var s3Client = new AmazonS3Client(credentials, RegionEndpoint.EUWest1);
            //var s3Client = new AmazonS3Client("AKIA5ZBDVSMBGSMQROHJ", "OHChb0GRfKZoq7/4zXgxbh84fuF/EBFcTbXxRctl", RegionEndpoint.EUWest1);

            var stream =
                new FileStream(@"C:\Users\josep\Documents\testImage.jpg", FileMode.Open);

            var putRequest = new PutObjectRequest
            {
                BucketName = "webplatform-documents",
                Key = "testfile.zip",
                InputStream = stream
            };

            try
            {
                var response2 = s3Client.PutObjectAsync(putRequest).Result;
                
            }
            catch (AggregateException e)
            {
                var ex = e;
            }
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get { return 32 + (int) (TemperatureC / 0.5556); }
            }
        }
    }
}