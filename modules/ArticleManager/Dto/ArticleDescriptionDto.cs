using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ArticleManager.Models;

namespace ArticleManager.Dto;

public class ArticleDescriptionDto
{
    [Required]
    [JsonPropertyName("author")]
    public string Author { get; set; } = null!;

    [Required]
    [JsonPropertyName("position")]
    public string Position { get; set; } = null!;

    [Required]
    [JsonPropertyName("university")]
    public string University { get; set; } = null!;

    [Required]
    [JsonPropertyName("faculty")]
    public string Faculty { get; set; } = null!;

    [Required]
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [Required]
    [JsonPropertyName("annotation")]
    public string Annotation { get; set; } = null!;

    [Required]
    [JsonPropertyName("content")]
    public string Content { get; set; } = null!;
    
    [Required]
    [JsonPropertyName("degree")]
    public Degree Degree { get; set; }
    
    [Required]
    [JsonPropertyName("element")]
    public Element Element { get; set; }
    
    [Required]
    [JsonPropertyName("form")]
    public Form Form { get; set; }
    
    [Required]
    [JsonPropertyName("format")]
    public Format Format { get; set; }
    
    [Required]
    [JsonPropertyName("variant")]
    public Variant Variant { get; set; }
    
    [Required]
    [JsonPropertyName("application")]
    public Application Application { get; set; }
    
    [Required]
    [JsonPropertyName("blum")]
    public int Blum { get; set; }
    
    [Required]
    [JsonPropertyName("difficulty")]
    public int Difficulty { get; set; }
}
