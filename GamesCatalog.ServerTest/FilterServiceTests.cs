using GamesCatalog.Server.Data;
using GamesCatalog.Server.Services;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace GamesCatalog.ServerTest;

[TestFixture]
public class FilterServiceTests
{
    private FilterService _filterService;
    private IQueryable<Game> _games;

    [SetUp]
    public void SetUp()
    {
        Tag tag1 = new() { Id = 1 };
        Tag tag2 = new() { Id = 2 };

        Platform platform1 = new() { Id = 1 };
        Platform platform2 = new() { Id = 2 };

        _filterService = new FilterService();
        _games = new List<Game>
        {
            new Game { Id = 1, Title = "Game A", Rating = 85, YearOfRelease = 2020, Price = 59.99, IsReleased = true, DeveloperId = 1, PublisherId = 1,
                Tags = [tag1],
                Platforms = [platform1],

                CatalogsLinks = [new CatalogLink { CatalogId = 1, GameId = 1 }] },
            new Game { Id = 2, Title = "Game B", Rating = 90, YearOfRelease = 2021, Price = 49.99, IsReleased = false, DeveloperId = 2, PublisherId = 2,
                Tags = [tag2],
                Platforms = [platform2],
                CatalogsLinks = [new CatalogLink { CatalogId = 2, GameId = 2 }] },

            new Game { Id = 3, Title = "Game C", Rating = 75, YearOfRelease = 2019, Price = 39.99, IsReleased = true, DeveloperId = 1, PublisherId = 2,
                Tags = [tag1, tag2],
                Platforms = [platform1, platform2],
                CatalogsLinks = [new CatalogLink { CatalogId = 1, GameId = 3 }, new CatalogLink { CatalogId = 2, GameId = 3 }] }
        }.AsQueryable();
    }

    [Test]
    public void FilterByTags_ShouldReturnCorrectGames()
    {
        var result = _filterService.FilterByTags(_games, new List<int> { 1 }).ToList();
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result.Any(g => g.Id == 1), Is.True);
        Assert.That(result.Any(g => g.Id == 3), Is.True);
    }

    [Test]
    public void FilterByTags_ShouldReturnEmpty_WhenNoMatch()
    {
        var result = _filterService.FilterByTags(_games, new List<int> { 3 }).ToList();
        Assert.That(result.Count, Is.EqualTo(0));
    }

    [Test]
    public void FilterByPlatforms_ShouldReturnCorrectGames()
    {
        var result = _filterService.FilterByPlatforms(_games, new List<int> { 1 }).ToList();
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result.Any(g => g.Id == 1), Is.True);
        Assert.That(result.Any(g => g.Id == 3), Is.True);
    }

    [Test]
    public void FilterByPlatforms_ShouldReturnEmpty_WhenNoMatch()
    {
        var result = _filterService.FilterByPlatforms(_games, new List<int> { 3 }).ToList();
        Assert.That(result.Count, Is.EqualTo(0));
    }

    [Test]
    public void FilterByCatalogs_ShouldReturnCorrectGames()
    {
        var result = _filterService.FilterByCatalogs(_games, new List<int> { 1 }).ToList();
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result.Any(g => g.Id == 1), Is.True);
        Assert.That(result.Any(g => g.Id == 3), Is.True);
    }

    [Test]
    public void FilterByCatalogs_ShouldReturnEmpty_WhenNoMatch()
    {
        var result = _filterService.FilterByCatalogs(_games, new List<int> { 3 }).ToList();
        Assert.That(result.Count, Is.EqualTo(0));
    }

    [Test]
    public void FilterByDevelopers_ShouldReturnCorrectGames()
    {
        var result = _filterService.FilterByDevelopers(_games, new List<int> { 1 }).ToList();
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result.Any(g => g.Id == 1), Is.True);
        Assert.That(result.Any(g => g.Id == 3), Is.True);
    }

    [Test]
    public void FilterByDevelopers_ShouldReturnEmpty_WhenNoMatch()
    {
        var result = _filterService.FilterByDevelopers(_games, new List<int> { 3 }).ToList();
        Assert.That(result.Count, Is.EqualTo(0));
    }

    [Test]
    public void FilterByPublishers_ShouldReturnCorrectGames()
    {
        var result = _filterService.FilterByPublishers(_games, new List<int> { 2 }).ToList();
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result.Any(g => g.Id == 2), Is.True);
        Assert.That(result.Any(g => g.Id == 3), Is.True);
    }

    [Test]
    public void FilterByPublishers_ShouldReturnEmpty_WhenNoMatch()
    {
        var result = _filterService.FilterByPublishers(_games, new List<int> { 3 }).ToList();
        Assert.That(result.Count, Is.EqualTo(0));
    }

    [Test]
    public void FilterByRating_ShouldReturnCorrectGames()
    {
        var result = _filterService.FilterByRating(_games, 80, 100).ToList();
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result.Any(g => g.Id == 1), Is.True);
        Assert.That(result.Any(g => g.Id == 2), Is.True);
    }

    [Test]
    public void FilterByRating_ShouldReturnEmpty_WhenNoMatch()
    {
        var result = _filterService.FilterByRating(_games, 95, 100).ToList();
        Assert.That(result.Count, Is.EqualTo(0));
    }

    [Test]
    public void FilterByYear_ShouldReturnCorrectGames()
    {
        var result = _filterService.FilterByYear(_games, 2020, 2021).ToList();
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result.Any(g => g.Id == 1), Is.True);
        Assert.That(result.Any(g => g.Id == 2), Is.True);
    }

    [Test]
    public void FilterByYear_ShouldReturnEmpty_WhenNoMatch()
    {
        var result = _filterService.FilterByYear(_games, 2022, 2023).ToList();
        Assert.That(result.Count, Is.EqualTo(0));
    }

    [Test]
    public void FilterByPrice_ShouldReturnCorrectGames()
    {
        var result = _filterService.FilterByPrice(_games, 40.00, 60.00).ToList();
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result.Any(g => g.Id == 1), Is.True);
        Assert.That(result.Any(g => g.Id == 2), Is.True);
    }

    [Test]
    public void FilterByPrice_ShouldReturnEmpty_WhenNoMatch()
    {
        var result = _filterService.FilterByPrice(_games, 60.01, 100.00).ToList();
        Assert.That(result.Count, Is.EqualTo(0));
    }

    [Test]
    public void FilterByReleaseStatus_ShouldReturnCorrectGames()
    {
        var result = _filterService.FilterByReleaseStatus(_games, true).ToList();
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result.Any(g => g.Id == 1), Is.True);
        Assert.That(result.Any(g => g.Id == 3), Is.True);
    }

    [Test]
    public void FilterByReleaseStatus_ShouldReturnEmpty_WhenNoMatch()
    {
        var result = _filterService.FilterByReleaseStatus(_games, false).ToList();
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result.Any(g => g.Id == 2), Is.True);
    }

    [Test]
    public void FilterByAvailabilityOfDLC_ShouldReturnCorrectGames()
    {
        var result = _filterService.FilterByAvailabilityOfDLC(_games, false).ToList();
        Assert.That(result.Count, Is.EqualTo(3));
    }

    [Test]
    public void FilterByAvailabilityOfDLC_ShouldReturnEmpty_WhenNoMatch()
    {
        var result = _filterService.FilterByAvailabilityOfDLC(_games, true).ToList();
        Assert.That(result.Count, Is.EqualTo(0));
    }

    [Test]
    public void FilterOutDLCs_ShouldReturnCorrectGames()
    {
        var result = _filterService.FilterOutDLCs(_games).ToList();
        Assert.That(result.Count, Is.EqualTo(3));
    }

    [Test]
    public void FilterOutDLCs_ShouldReturnEmpty_WhenAllAreDLCs()
    {
        var games = new List<Game>
        {
            new Game { Id = 4, Title = "Main Game" },
            new Game { Id = 5, Title = "DLC 1", ParentGameId = 5 },
            new Game { Id = 6, Title = "DLC 2", ParentGameId = 6 }
        }.AsQueryable();

        var result = _filterService.FilterOutDLCs(games).ToList();
        Assert.That(result.Count, Is.EqualTo(1));
    }
}
