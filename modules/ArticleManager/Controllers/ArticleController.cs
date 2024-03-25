using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ArticleManager.Dto;
using ArticleManager.Models;
using ArticleManager.Services;
using Microsoft.AspNetCore.Mvc;
using YandexCloud.Clients;

namespace ArticleManager.Controllers; 

[ApiController]
[Route("article")]
public class ArticleController : Controller
{
    private const string DownloadFilePath = "http://localhost:8080/article/download";
    
    private readonly IYandexCloudStorageClient _yandexCloudStorageClient;
    private readonly IArticleRepository _articleRepository;
    
    public ArticleController(
        IYandexCloudStorageClient yandexCloudStorageClient,
        IArticleRepository articleRepository)
    {
        _yandexCloudStorageClient = yandexCloudStorageClient;
        _articleRepository = articleRepository;
    }
    
    [HttpPost("upload")]
    public async Task<IActionResult> Upload(
        [FromBody] UploadArticleRequest request,
        CancellationToken cancellationToken)
    {
        if (request.ArticleFile is not null)
        {
            await _yandexCloudStorageClient.UploadFile(
                request.ArticleFile.Name,
                request.ArticleFile.Content,
                cancellationToken);
        }

        var article = new Article
        {
            Author = request.ArticleDescription.Author,
            Position = request.ArticleDescription.Position,
            University = request.ArticleDescription.University,
            Faculty = request.ArticleDescription.Faculty,
            Name = request.ArticleDescription.Name,
            Annotation = request.ArticleDescription.Annotation,
            Content = request.ArticleDescription.Content,
            Date = DateTime.Now,
            Link = request.ArticleFile is null ? null : GetFileDownloadLink(request.ArticleFile.Name),
            Degree = request.ArticleDescription.Degree,
            Element = request.ArticleDescription.Element,
            Form = request.ArticleDescription.Form,
            Format = request.ArticleDescription.Format,
            Variant = request.ArticleDescription.Variant,
            Application = request.ArticleDescription.Application,
            Blum = request.ArticleDescription.Blum,
            Difficulty = request.ArticleDescription.Difficulty,
        };

        await _articleRepository.AddArticle(article, cancellationToken);
        
        return Ok();
    }

    [HttpGet("get")]
    public async Task<IActionResult> Get(
        [FromQuery] int id,
        CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetArticle(id, cancellationToken);

        if (article is null)
        {
            return NotFound();
        }
        
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

    private static string GetFileDownloadLink(string key) => $"${DownloadFilePath}?key={key}";
}