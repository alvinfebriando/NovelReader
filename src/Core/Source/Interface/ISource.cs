using NovelReader.Core.Source.Model;

namespace NovelReader.Core.Source.Interface;

public interface ISource
{
    public Task<NovelsPage> FetchNovelList(int page, Query query);
}