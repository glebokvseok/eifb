using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SearchService.Dto;

namespace SearchService.Services;

public interface ISearchRepository
{
    Task<IReadOnlyList<int>> SearchArticles(SearchArticleRequest searchParams, CancellationToken cancellationToken);
}