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
        return query.Skip((page - 1) * gamesPerPage).Take(gamesPerPage);
    }
}
