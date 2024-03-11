using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ArticleManager.Dto;

public class ArticleFileDto
{
    [Required]
    [JsonPropertyName("name")] 
    public string Name { get; set; } = null!;

    [Required]
    [JsonPropertyName("content")] 
    public string Content { get; set; } = null!;
}