using SearchService.Dto;

namespace SearchService.Services;

public interface ISearchRequestBuilder
{
    string BuildSearchRequest(SearchArticleRequest request);
}