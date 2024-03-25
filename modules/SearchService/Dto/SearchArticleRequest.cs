using System;
using System.Collections.Generic;
using ArticleManager.Contracts.Enums;

namespace SearchService.Dto;

public class SearchArticleRequest
{
    public string? Name { get; set; }
    
    public DateTime? Date { get; set; }
    
    public string? Author { get; set; }
    
    public int[]? Degrees { get; set; }
    
    public int[]? Elements { get; set; }
    
    public int[]? Formats { get; set; }
}