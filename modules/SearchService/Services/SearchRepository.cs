using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using SearchService.Dto;

namespace SearchService.Services;

public class SearchRepository : ISearchRepository
{
    private readonly ISearchRequestBuilder _searchRequestBuilder;
    private readonly SearchRepositoryConfig _config;
    
    public SearchRepository(
        ISearchRequestBuilder searchRequestBuilder,
        IOptions<SearchRepositoryConfig> config)
    {
        _searchRequestBuilder = searchRequestBuilder;
        _config = config.Value;
    }
    
    public async Task<IReadOnlyList<int>> SearchArticles(
        SearchArticleRequest searchParams,
        CancellationToken cancellationToken)
    {
        var request = _searchRequestBuilder.BuildSearchRequest(searchParams);
        
        await using var connection = new NpgsqlConnection(_config.ConnectionString);
        await connection.OpenAsync(cancellationToken);
        return (await connection.QueryAsync<int>(request, searchParams)).AsList();
    }
}