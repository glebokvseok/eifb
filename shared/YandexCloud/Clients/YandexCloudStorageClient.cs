using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;
using YandexCloud.Models;

namespace YandexCloud.Clients;

public class YandexCloudStorageClient : IYandexCloudStorageClient
{
    private readonly YandexCloudStorageClientConfig _config;
    
    public YandexCloudStorageClient(
        IOptions<YandexCloudStorageClientConfig> config)
    {
        _config = config.Value;
    }

    public async Task UploadFile(string key, string content, CancellationToken cancellationToken)
    {
        var client = GetYandexCloudClient();

        var request = new PutObjectRequest
        {
            BucketName = _config.BucketName,
            Key = key,
            InputStream = new MemoryStream(Convert.FromBase64String(content)),
        };

        await client.PutObjectAsync(request, cancellationToken);
    }

    public async Task<YandexCloudStorageFile> DownloadFile(string key, CancellationToken cancellationToken)
    {
        var client = GetYandexCloudClient();
        
        using var response = await client.GetObjectAsync(_config.BucketName, key, cancellationToken);
        
        var memoryStream = new MemoryStream();
        await response.ResponseStream.CopyToAsync(memoryStream, cancellationToken);
        memoryStream.Seek(0, SeekOrigin.Begin);

        return new YandexCloudStorageFile
        {
            Name = response.Key,
            ContentType = response.Headers.ContentType,
            DataStream = memoryStream,
        };
    }

    private AmazonS3Client GetYandexCloudClient()
    {
        var config = new AmazonS3Config
        {
            ServiceURL = _config.ServiceUrl,
            AuthenticationRegion = _config.AuthenticationRegion,
        };

        var credentials = new BasicAWSCredentials(_config.AccessKey, _config.SecretKey);

        return new AmazonS3Client(credentials, config);
    }
}