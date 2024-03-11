using System.Collections.Generic;
using ArticleManager.Dto;

namespace ArticleManager;

public class StorageKeeper : IStorageKeeper
{
    public StorageKeeper()
    {
        Storage = new Dictionary<int, UploadArticleRequest>();
    }
    
    public IDictionary<int, UploadArticleRequest> Storage { get; }
}