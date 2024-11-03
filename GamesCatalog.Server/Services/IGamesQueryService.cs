namespace GamesCatalog.Server.Services;

public interface IGamesQueryService
{
    public IQueryable<Game> GetGamesQuery();
    public IQueryable<Game> GetGamesWithDLCsQuery();
    public IQueryable<Game> Paginate(IQueryable<Game> query, int gamesPerPage, int page);
}
