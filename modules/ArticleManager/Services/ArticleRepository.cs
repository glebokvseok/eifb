using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ArticleManager.Models;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;

namespace ArticleManager.Services;

public class ArticleRepository : IArticleRepository
{
    private readonly ArticleRepositoryConfig _config;
    
    public ArticleRepository(IOptions<ArticleRepositoryConfig> config)
    {
        _config = config.Value;
    }

    public async Task<bool> AddArticle(Article article, CancellationToken cancellationToken)
    {
        const string request =
            "INSERT INTO articles (author, position, university, faculty, name, annotation, content, date, link, degree, element, form, format, variant, application, blum, difficulty) " +
            "VALUES (@author, @position, @university, @faculty, @name, @annotation, @content, @date, @link, @degree, @element, @form, @format, @variant, @application, @blum, @difficulty);";
        
        await using var connection = new NpgsqlConnection(_config.ConnectionString);
        await connection.OpenAsync(cancellationToken);
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);
        try {
            await connection.ExecuteAsync(request, article);
            await transaction.CommitAsync(cancellationToken);
            return true;
        } catch {
            await transaction.RollbackAsync(cancellationToken);
        }
        
        return false;
    }

    public async Task<Article?> GetArticle(int id, CancellationToken cancellationToken)
    {
        const string request = "SELECT * FROM articles WHERE id = @id";
        
        await using var connection = new NpgsqlConnection(_config.ConnectionString);
        await connection.OpenAsync(cancellationToken);
        return (await connection.QueryAsync<Article?>(request, new { id })).FirstOrDefault();
    }
}