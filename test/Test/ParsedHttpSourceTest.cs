using NovelReader.Core.Source;
using NovelReader.Core.Source.Model;
using NovelReader.Extension;
using Xunit.Abstractions;

namespace NovelReader.Test;

public class MadaraTest
{
    private readonly ParsedHttpSource _sut;
    private readonly ITestOutputHelper _output;

    public MadaraTest(ITestOutputHelper output)
    {
        _output = output;
        _sut = new Madara { BaseUrl = new Uri("https://boxnovel.com") };
    }

    [Fact]
    public async void TestFetchNovelListOrderByLatest()
    {
        // Arrange
        var query = new Query(false, OrderBy.Latest);
        // Act
        var np = await _sut.FetchNovelList(1, query);
        // Assert
        Assert.Equal(10, np.Novels.ToList().Count);
    }
    
    [Fact]
    public async void TestFetchNovelListOrderByPopular()
    {
        // Arrange
        var query = new Query(false, OrderBy.Popular);
        // Act
        var np = await _sut.FetchNovelList(1, query);
        // Assert
        Assert.Equal(10, np.Novels.ToList().Count);
        Assert.Equal("Reincarnation Of The Strongest Sword God", np.Novels.First().Title);
    }

    [Fact]
    public async void TestFetchNovelListBySearching()
    {
        // Arrange
        var query = new Query { IsSearching = true, Name = "legendary mechanic" };
        // Act
        var np = await _sut.FetchNovelList(1, query);
        // Assert
        Assert.Single(np.Novels);
        Assert.Equal("The Legendary Mechanic", np.Novels.First().Title);
    }

    [Fact]
    public async void TestLoadExtension()
    {
        
    }
}