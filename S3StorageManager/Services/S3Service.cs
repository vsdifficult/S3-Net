using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace S3Api.Services
{
    public class S3Service : IS3Service
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

        public S3Service(IConfiguration configuration)
        {
            var accessKey = configuration["AWS:AccessKey"];
            var secretKey = configuration["AWS:SecretKey"];
            var region = RegionEndpoint.GetBySystemName(configuration["AWS:Region"]);
            _bucketName = configuration["AWS:BucketName"];

            _s3Client = new AmazonS3Client(accessKey, secretKey, region);
        }

        public async Task UploadFileAsync(string filePath, string keyName)
        {
            var putRequest = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = keyName,
                FilePath = filePath
            };

            await _s3Client.PutObjectAsync(putRequest);
        }

        public async Task DownloadFileAsync(string keyName, string destinationPath)
        {
            var response = await _s3Client.GetObjectAsync(_bucketName, keyName);
            await using var responseStream = response.ResponseStream;
            await using var fileStream = File.Create(destinationPath);
            await responseStream.CopyToAsync(fileStream);
        }

        public async Task DeleteFileAsync(string keyName)
        {
            await _s3Client.DeleteObjectAsync(_bucketName, keyName);
        }

        public async Task ListFilesAsync()
        {
            var request = new ListObjectsV2Request { BucketName = _bucketName };
            var response = await _s3Client.ListObjectsV2Async(request);

            foreach (var entry in response.S3Objects)
            {
                Console.WriteLine($"- {entry.Key} (Size: {entry.Size})");
            }
        }
    }
}
