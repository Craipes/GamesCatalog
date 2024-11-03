namespace GamesCatalog.Server.Services;

public class FilterService : IFilterService
{
    public IQueryable<Game> FilterByTitle(IQueryable<Game> games, string title, bool caseInsensetive)
    {
        if (caseInsensetive)
        {
            title = title.ToUpper();
            return games.Where(g => g.Title.ToUpper().Contains(title));
        }
        return games.Where(g => g.Title.Contains(title));
    }

    public IQueryable<Game> FilterByTags(IQueryable<Game> games, IEnumerable<int> tags)
    {
        return games.Where(g => tags.All(t => g.Tags.Any(a => a.Id == t)));
    }

    public IQueryable<Game> FilterByPlatforms(IQueryable<Game> games, IEnumerable<int> platforms)
    {
        return games.Where(g => g.Platforms.Any(p => platforms.Contains(p.Id)));
    }

    public IQueryable<Game> FilterByCatalogs(IQueryable<Game> games, IEnumerable<int> catalogs)
    {
        return games.Where(g => g.CatalogsLinks.Any(c => catalogs.Contains(c.CatalogId)));
    }

    public IQueryable<Game> FilterByDevelopers(IQueryable<Game> games, IEnumerable<int> developers)
    {
        return games.Where(g => developers.Contains(g.DeveloperId ?? -1));
    }

    public IQueryable<Game> FilterByPublishers(IQueryable<Game> games, IEnumerable<int> publishers)
    {
        return games.Where(g => publishers.Contains(g.PublisherId ?? -1));
    }

    public IQueryable<Game> FilterByRating(IQueryable<Game> games, int minRating, int maxRating)
    {
        return games.Where(g => g.Rating >= minRating && g.Rating <= maxRating);
    }

    public IQueryable<Game> FilterByYear(IQueryable<Game> games, int minYear, int maxYear)
    {
        return games.Where(g => g.YearOfRelease >= minYear && g.YearOfRelease <= maxYear);
    }

    public IQueryable<Game> FilterByPrice(IQueryable<Game> games, double minPrice, double maxPrice)
    {
        return games.Where(g => g.Price >= minPrice && g.Price <= maxPrice);
    }

    public IQueryable<Game> FilterByReleaseStatus(IQueryable<Game> games, bool isReleased)
    {
        return games.Where(g => g.IsReleased == isReleased);
    }

    public IQueryable<Game> FilterByAvailabilityOfDLC(IQueryable<Game> games, bool isDLC)
    {
        return games.Where(g => (g.DLCs.Count != 0) == isDLC);
    }

    public IQueryable<Game> FilterOutDLCs(IQueryable<Game> games)
    {
        return games.Where(g => g.ParentGameId == null);
    }
}
