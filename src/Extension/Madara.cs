using Flurl;
using Flurl.Http;
using Humanizer;
using NovelReader.Core.Source;
using NovelReader.Core.Source.Model;

namespace NovelReader.Extension;

public class Madara : ParsedHttpSource
{
    public override Guid Id { get; set; }
    public override string Name { get; set; }
    public override Uri BaseUrl { get; set; }

    public override async Task<string> Request(int page, Query query)
    {
        var order = query.OrderBy switch
        {
            OrderBy.Popular => "views",
            OrderBy.Latest => "latest",
            OrderBy.Alphabet => "alphabet",
            _ => throw new ArgumentOutOfRangeException(nameof(query), query, null)
        };

        if (!query.IsSearching)
        {
            return await BaseUrl
                .AppendPathSegments("novel", "page", page.ToString())
                .SetQueryParams(new { m_orderby = order })
                .GetStringAsync();
        }

        var genreQuery =
            query.Genres?.Select(g => g.ToString().Humanize(LetterCasing.LowerCase));
        var statusQuery =
            query.Statuses?.Select(s => s.ToString().Humanize(LetterCasing.LowerCase));
        var otherQuery = new
        {
            s = query.Name,
            post_type = "wp-manga",
            op = query.Operation,
            author = query.Author,
            release = query.ReleaseYear,
            adult = query.AdultContent,
            m_orderby = order
        };

        return await BaseUrl
            .AppendPathSegments("novel", "page", page.ToString())
            .SetQueryParam("genre[]", genreQuery)
            .SetQueryParam("status[]", statusQuery)
            .SetQueryParams(otherQuery)
            .GetStringAsync();
    }
    public override string NovelListSelector()
    {
        return ".page-item-detail";
    }

    public override string SearchedNovelListSelector()
    {
        return ".c-tabs-item__content";
    }

    public override string NextPageSelector()
    {
        return "nav.paging-navigation .nav-previous a";
    }

    public override string SearchedNextPageSelector()
    {
        return NextPageSelector();
    }

    public override string TitleUrlFromNovelListSelector()
    {
        return ".post-title a";
    }

    public override string TitleUrlFromSearchedNovelListSelector()
    {
        return TitleUrlFromNovelListSelector();
    }

    public override string ThumbnailUrlFromNovelListSelector()
    {
        return ".item-thumb img";
    }

    public override string ThumbnailUrlFromSearchedNovelListSelector()
    {
        return ".c-image-hover img";
    }
}