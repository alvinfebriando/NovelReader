namespace NovelReader.Core.Source.Model;

public class Chapter
{
    public Uri Url { get; set; }
    public string Name { get; set; }
    public DateTime? DateUpload { get; set; }
    public int? ChapterNumber { get; set; }
}