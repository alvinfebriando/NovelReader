using AngleSharp.Dom;
using NovelReader.Core.Source.Model;

namespace NovelReader.Core.Source.Interface;

public interface INovelParser
{
    public Task<NovelsPage> ParseNovel(string html, bool isSearching);
    public Novel GetNovelFromElement(IElement element, string titleUrlSelector, string thumbnailUrlSelector);
}