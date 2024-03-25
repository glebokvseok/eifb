using System;

namespace ArticleManager.Models;

public class Article
{
    public string Author { get; set; } = null!;

    public string Position { get; set; } = null!;

    public string University { get; set; } = null!;

    public string Faculty { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Annotation { get; set; } = null!;

    public string Content { get; set; } = null!;
    
    public DateTime Date { get; set; }

    public string? Link { get; set; }
    
    public Degree Degree { get; set; }
    
    public Element Element { get; set; }
    
    public Form Form { get; set; }
    
    public Format Format { get; set; }
    
    public Variant Variant { get; set; }
    
    public Application Application { get; set; }
    
    public int Blum { get; set; }
    
    public int Difficulty { get; set; }
}