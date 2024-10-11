namespace GamesCatalog.Server.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class AppController : Controller
{
    private readonly GamesDbContext _context;

    public AppController(GamesDbContext context)
    {
        _context = context;
    }

    [HttpGet("search")]
    public async Task<IEnumerable<GameDto>> Search([FromQuery] string? search = null, [FromQuery] string? tags = null, [FromQuery] string? platforms = null,
        [FromQuery] string? catalogs = null, [FromQuery] string? developers = null, [FromQuery] string? publishers = null, [FromQuery] OrderingType ordering = OrderingType.Default,
        [FromQuery] int minRating = 0, [FromQuery] int maxRating = 100, [FromQuery] int minYear = 0, [FromQuery] int maxYear = 10000, [FromQuery] bool? dlc = null)
    {
        IQueryable<Game> request = _context.Games
            .Include(g => g.Publisher)
            .Include(g => g.Developer)
            .Include(g => g.Tags)
            .Include(g => g.Platforms)
            .Include(g => g.CatalogsLinks);

        if (!string.IsNullOrEmpty(search))
        {
            request = request.Where(g => g.Title.Contains(search));
        }
        if (!string.IsNullOrEmpty(tags))
        {
            if (TryParseStringToIntArray(tags, out var tagsList))
                request = request.Where(g => tagsList.All(t => g.Tags.Any(a => a.Id == t)));
        }
        if (!string.IsNullOrEmpty(platforms))
        {
            if (TryParseStringToIntArray(platforms, out var platformsList))
                request = request.Where(g => g.Platforms.Any(p => platformsList.Contains(p.Id)));
        }
        if (!string.IsNullOrEmpty(catalogs))
        {
            if (TryParseStringToIntArray(catalogs, out var catalogsList))
                request = request.Where(g => g.CatalogsLinks.Any(c => catalogsList.Contains(c.CatalogId)));
        }
        if (!string.IsNullOrEmpty(developers))
        {
            if (TryParseStringToIntArray(developers, out var developersList))
                request = request.Where(g => developersList.Contains(g.DeveloperId ?? 0));
        }
        if (!string.IsNullOrEmpty(publishers))
        {
            if (TryParseStringToIntArray(publishers, out var publishersList))
                request = request.Where(g => publishersList.Contains(g.PublisherId ?? 0));
        }
        request = request.Where(g => g.Rating >= minRating && g.Rating <= maxRating);
        request = request.Where(g => g.YearOfRelease >= minYear && g.YearOfRelease <= maxYear);

        // DLC + Price + Pagination

        request = ordering switch
        {
            OrderingType.TitleAsc => request.OrderBy(g => g.Title),
            OrderingType.TitleDesc => request.OrderByDescending(g => g.Title),
            OrderingType.YearAsc => request.OrderBy(g => g.YearOfRelease),
            OrderingType.YearDesc => request.OrderByDescending(g => g.YearOfRelease),
            OrderingType.RatingAsc => request.OrderBy(g => g.Rating),
            OrderingType.RatingDesc => request.OrderByDescending(g => g.Rating),
            _ => request
        };

        var games = await request.ToListAsync();
        return games.Select(GameDto.FromGame);
    }

    private static bool TryParseStringToIntArray(string value, out int[] result)
    {
        if (string.IsNullOrEmpty(value))
        {
            result = [];
            return true;
        }
        var values = value.Split(',');
        result = new int[values.Length];
        for (int i = 0; i < values.Length; i++)
        {
            if (!int.TryParse(values[i], out result[i])) return false;
        }
        return true;
    }
}
