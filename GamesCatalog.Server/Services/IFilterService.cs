namespace GamesCatalog.Server.Services;

public interface IFilterService
{
    public IQueryable<Game> FilterByTags(IQueryable<Game> games, IEnumerable<int> tags);
    public IQueryable<Game> FilterByPlatforms(IQueryable<Game> games, IEnumerable<int> platforms);
    public IQueryable<Game> FilterByCatalogs(IQueryable<Game> games, IEnumerable<int> catalogs);
    public IQueryable<Game> FilterByDevelopers(IQueryable<Game> games, IEnumerable<int> developers);
    public IQueryable<Game> FilterByPublishers(IQueryable<Game> games, IEnumerable<int> publishers);
    public IQueryable<Game> FilterByRating(IQueryable<Game> games, int minRating, int maxRating);
    public IQueryable<Game> FilterByYear(IQueryable<Game> games, int minYear, int maxYear);
    public IQueryable<Game> FilterByPrice(IQueryable<Game> games, double minPrice, double maxPrice);
    public IQueryable<Game> FilterByReleaseStatus(IQueryable<Game> games, bool isReleased);
    public IQueryable<Game> FilterByAvailabilityOfDLC(IQueryable<Game> games, bool isDLC);
    public IQueryable<Game> FilterOutDLCs(IQueryable<Game> games);
}
