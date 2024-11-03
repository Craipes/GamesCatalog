namespace GamesCatalog.Server.Services;

public class GamesQueryService : IGamesQueryService
{
    private readonly GamesDbContext _context;

    public GamesQueryService(GamesDbContext context)
    {
        _context = context;
    }

    public IQueryable<Game> GetGamesQuery()
    {
        return _context.Games
            .AsNoTracking()
            .Include(g => g.Developer)
            .Include(g => g.Publisher)
            .Include(g => g.Platforms)
            .Include(g => g.Tags)
            .Include(g => g.CatalogsLinks)
                .ThenInclude(l => l.Catalog);
    }

    public IQueryable<Game> GetGamesWithDLCsQuery()
    {
        return GetGamesQuery()
            .Include(g => g.DLCs).ThenInclude(g => g.Publisher)
            .Include(g => g.DLCs).ThenInclude(g => g.Developer)
            .Include(g => g.DLCs).ThenInclude(g => g.Tags)
            .Include(g => g.DLCs).ThenInclude(g => g.Platforms)
            .Include(g => g.DLCs).ThenInclude(g => g.CatalogsLinks);
    }

    public IQueryable<Game> Paginate(IQueryable<Game> query, int gamesPerPage, int page)
    {
        gamesPerPage = Math.Max(1, gamesPerPage);
        page = Math.Max(1, page);
        return query.Skip((page - 1) * gamesPerPage).Take(gamesPerPage);
    }

    public IQueryable<Game> Order(IQueryable<Game> query, OrderingType ordering)
    {
        return ordering switch
        {
            OrderingType.TitleAsc => query.OrderBy(g => g.Title),
            OrderingType.TitleDesc => query.OrderByDescending(g => g.Title),
            OrderingType.YearAsc => query.OrderBy(g => g.YearOfRelease),
            OrderingType.YearDesc => query.OrderByDescending(g => g.YearOfRelease),
            OrderingType.RatingAsc => query.OrderBy(g => g.Rating),
            OrderingType.RatingDesc => query.OrderByDescending(g => g.Rating),
            _ => query
        };
    }
}
