using System.Threading;
using System.Threading.Tasks;
using YandexCloud.Models;

namespace YandexCloud.Clients;

public interface IYandexCloudStorageClient
{
    Task<YandexCloudStorageFile> DownloadFile(string key, CancellationToken cancellationToken);

    Task UploadFile(string key, string content, CancellationToken cancellationToken);
}