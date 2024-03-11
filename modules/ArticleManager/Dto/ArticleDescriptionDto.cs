using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ArticleManager.Dto;

public class ArticleDescriptionDto
{
    [Required]
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;
    
    [JsonPropertyName("author")]
    public string Author { get; set; } = null!;
    
    [JsonPropertyName("description")]
    public string Description { get; set; } = null!;
}