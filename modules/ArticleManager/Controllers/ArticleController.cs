using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ArticleManager.Dto;
using Microsoft.AspNetCore.Mvc;
using YandexCloud.Clients;

namespace ArticleManager.Controllers; 

[ApiController]
[Route("article")]
public class ArticleController : Controller
{
    private readonly IYandexCloudStorageClient _yandexCloudStorageClient;
    private readonly IDictionary<int, UploadArticleRequest> _storage; // temporary
    
    public ArticleController(
        IYandexCloudStorageClient yandexCloudStorageClient,
        IStorageKeeper storageKeeper)
    {
        _yandexCloudStorageClient = yandexCloudStorageClient;
        _storage = storageKeeper.Storage;
    }
    
    [HttpPost("upload")]
    public async Task<IActionResult> Upload(
        [FromBody] UploadArticleRequest request,
        CancellationToken cancellationToken)
    {
        if (_storage.ContainsKey(request.ArticleDescription.Id))
        {
            return BadRequest("Article with this id already exists");
        }
        
        _storage.Add(request.ArticleDescription.Id, request);

        if (request.ArticleFile is not null)
        {
            await _yandexCloudStorageClient.UploadFile(
                request.ArticleFile.Name,
                request.ArticleFile.Content,
                cancellationToken);
        }
        
        return Ok();
    }

    [HttpGet("get")]
    public async Task<IActionResult> Get(
        [FromQuery] int id,
        CancellationToken cancellationToken)
    {
        if (!_storage.ContainsKey(id))
        {
            return NotFound("Article with this id does not exist");
        }

        _storage.TryGetValue(id, out var article);
        
        return Ok(article);
    }

    [HttpGet("download")]
    public async Task<IActionResult> Download(
        [FromQuery] string key,
        CancellationToken cancellationToken)
    {
        var file = await _yandexCloudStorageClient.DownloadFile(key, cancellationToken);

        return File(file.DataStream, file.ContentType, file.Name);
    }
}