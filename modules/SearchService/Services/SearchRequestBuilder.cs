using System.Text;
using Extensions;
using SearchService.Dto;

namespace SearchService.Services;

public class SearchRequestBuilder : ISearchRequestBuilder
{
    private const string BaseSearchRequest = "SELECT id FROM articles WHERE 1 = 1";
        
    public string BuildSearchRequest(SearchArticleRequest request)
    {
        var searchRequestBuilder = new StringBuilder(BaseSearchRequest);

        if (!request.Author.IsNullOrEmpty())
        {
            searchRequestBuilder.Append(" AND author = @author");
        }

        if (request.Date is not null)
        {
            searchRequestBuilder.Append(" AND date > @date");
        }

        if (!request.Name.IsNullOrEmpty())
        {
            searchRequestBuilder.Append(" AND name = @name");
        }

        if (!request.Degrees.IsNullOrEmpty())
        {
            searchRequestBuilder.Append(" AND degree = ANY(@degrees)");
        }

        if (!request.Elements.IsNullOrEmpty())
        {
            searchRequestBuilder.Append(" AND element = ANY(@elements)");
        }

        if (!request.Formats.IsNullOrEmpty())
        {
            searchRequestBuilder.Append(" AND format = ANY(@formats)");
        }
        
        searchRequestBuilder.Append(';');

        return searchRequestBuilder.ToString();
    }
}