using System;
using System.IO;

namespace YandexCloud.Models;

public class YandexCloudStorageFile
{
    public string Name { get; init; } = null!;
    
    public string ContentType { get; init; } = null!;
    
    public MemoryStream DataStream { get; init; } = null!;
}