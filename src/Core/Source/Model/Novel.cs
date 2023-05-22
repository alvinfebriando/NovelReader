namespace NovelReader.Core.Source.Model;

public class Novel
{
    public Uri Url { get; set; }
    public string Title { get; set; }
    public string? Author { get; set; }
    public string? Description { get; set; }
    public IEnumerable<Genre>? Genres { get; set; }
    public Uri? ThumbnailUrl { get; set; }
}
