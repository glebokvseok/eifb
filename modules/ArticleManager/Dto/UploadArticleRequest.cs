using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ArticleManager.Dto; 

public class UploadArticleRequest
{
    [Required]
    [JsonPropertyName("article_description")]
    public ArticleDescriptionDto ArticleDescription { get; set; } = null!;
    
    [JsonPropertyName("article_file")] 
    public ArticleFileDto? ArticleFile { get; set; }
}