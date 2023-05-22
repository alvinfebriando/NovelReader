namespace NovelReader.Core.Source.Model;

public class Query
{
    public Query()
    {
    }

    public Query(bool isSearching, OrderBy orderBy)
    {
        IsSearching = isSearching;
        OrderBy = orderBy;
    }

    public Query(
        string? name,
        bool isSearching,
        OrderBy orderBy,
        IEnumerable<Genre>? genres,
        Operation operation,
        string? author,
        int? releaseYear,
        AdultContent? adultContent,
        IEnumerable<Status>? statuses)
    {
        Name = name;
        IsSearching = isSearching;
        OrderBy = orderBy;
        Genres = genres;
        Operation = operation;
        Author = author;
        ReleaseYear = releaseYear;
        AdultContent = adultContent;
        Statuses = statuses;
    }

    public string? Name { get; set; }
    public bool IsSearching { get; set; } = false;
    public OrderBy OrderBy { get; set; }
    public IEnumerable<Genre>? Genres { get; set; }
    public Operation Operation { get; set; } = Operation.Or;
    public string? Author { get; set; }
    public int? ReleaseYear { get; set; }
    public AdultContent? AdultContent { get; set; }
    public IEnumerable<Status>? Statuses { get; set; }
}