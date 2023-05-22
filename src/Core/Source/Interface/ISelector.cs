namespace NovelReader.Core.Source.Interface;

public interface ISelector
{
    public string NovelListSelector();
    public string SearchedNovelListSelector();
    public string NextPageSelector();
    public string SearchedNextPageSelector();
    public string TitleUrlFromNovelListSelector();
    public string TitleUrlFromSearchedNovelListSelector();
    public string ThumbnailUrlFromNovelListSelector();
    public string ThumbnailUrlFromSearchedNovelListSelector();
}