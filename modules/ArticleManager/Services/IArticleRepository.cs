using System.Threading;
using System.Threading.Tasks;
using ArticleManager.Models;

namespace ArticleManager.Services;

public interface IArticleRepository
{
    Task<bool> AddArticle(Article article, CancellationToken cancellationToken);

    Task<Article?> GetArticle(int id, CancellationToken cancellationToken);
}