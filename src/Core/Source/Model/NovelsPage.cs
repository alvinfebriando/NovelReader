namespace NovelReader.Core.Source.Model;

public record NovelsPage(IEnumerable<Novel> Novels, bool HasNextPage);