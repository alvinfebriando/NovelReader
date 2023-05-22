using NovelReader.Core.Source.Model;

namespace NovelReader.Core.Source.Interface;

public interface INovelRequest
{
    public Task<string> Request(int page, Query query);
}