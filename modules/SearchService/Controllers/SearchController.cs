using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SearchService.Dto;
using SearchService.Services;

namespace SearchService.Controllers;

[ApiController]
[Route("search")]
public class SearchController : Controller
{
    private readonly ISearchRepository _searchRepository;
    
    public SearchController(ISearchRepository searchRepository)
    {
        _searchRepository = searchRepository;
    }
    
    [HttpPost("article")]
    public async Task <IActionResult> SearchArticle(
        [FromBody] SearchArticleRequest request,
        CancellationToken cancellationToken)
    {
        var articleIds = await _searchRepository
            .SearchArticles(request, cancellationToken);
        
        return Ok(articleIds);
    }
}