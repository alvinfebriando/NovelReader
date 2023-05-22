using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using NovelReader.Core.Source.Interface;
using NovelReader.Core.Source.Model;

namespace NovelReader.Core.Source;

public abstract class ParsedHttpSource : ISource, INovelRequest, INovelParser, ISelector
{
    public abstract Guid Id { get; set; }
    public abstract string Name { get; set; }
    public abstract Uri BaseUrl { get; set; }
    private readonly IHtmlParser? Parser;

    protected ParsedHttpSource()
    {
        var context = BrowsingContext.New();
        Parser = context.GetService<IHtmlParser>();
    }

    public async Task<NovelsPage> FetchNovelList(int page, Query query)
    {
        return await ParseNovel(await Request(page, query), query.IsSearching);
    }

    public abstract Task<string> Request(int page, Query query);

    public async Task<NovelsPage> ParseNovel(string html, bool isSearching)
    {
        var document = await Parser.ParseDocumentAsync(html);

        var novelListSelector = NovelListSelector();
        var titleUrlSelector = TitleUrlFromNovelListSelector();
        var thumbnailUrlSelector = ThumbnailUrlFromNovelListSelector();
        var nextPageSelector = NextPageSelector();

        if (isSearching)
        {
            novelListSelector = SearchedNextPageSelector();
            titleUrlSelector = TitleUrlFromSearchedNovelListSelector();
            thumbnailUrlSelector = ThumbnailUrlFromSearchedNovelListSelector();
            nextPageSelector = SearchedNextPageSelector();
        }

        var novels = document
            .QuerySelectorAll(novelListSelector)
            .Select(e => GetNovelFromElement(e, titleUrlSelector, thumbnailUrlSelector))
            .ToList();
        var hasNextPage = document.QuerySelector(nextPageSelector) != null;
        return new NovelsPage(novels, hasNextPage);
    }

    public Novel GetNovelFromElement(
        IElement element,
        string titleUrlSelector,
        string thumbnailUrlSelector)
    {
        var link = element.QuerySelector(titleUrlSelector);
        var thumbnailUrl = element.QuerySelector(thumbnailUrlSelector);
        return new Novel
        {
            Title = link.TextContent,
            Url = new Uri(link.GetAttribute("href") ?? throw new InvalidOperationException()),
            ThumbnailUrl = thumbnailUrl != null
                ? new Uri(
                    thumbnailUrl.GetAttribute("data-src") ?? throw new InvalidOperationException())
                : null
        };
    }

    public abstract string NovelListSelector();
    public abstract string SearchedNovelListSelector();

    public abstract string NextPageSelector();
    public abstract string SearchedNextPageSelector();

    public abstract string TitleUrlFromNovelListSelector();
    public abstract string TitleUrlFromSearchedNovelListSelector();

    public abstract string ThumbnailUrlFromNovelListSelector();
    public abstract string ThumbnailUrlFromSearchedNovelListSelector();
}