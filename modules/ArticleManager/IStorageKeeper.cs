using System.Collections.Generic;
using ArticleManager.Dto;

namespace ArticleManager;

public interface IStorageKeeper
{ 
    IDictionary<int, UploadArticleRequest> Storage { get; }
}