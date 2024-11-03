using GamesCatalog.Server.Services;

namespace GamesCatalog.Server.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class AppController : Controller
{
    private readonly IFilterService _filterService;

    private readonly GamesDbContext _context;

    public AppController(IFilterService filterService, GamesDbContext context)
    {
        _filterService = filterService;
        _context = context;
    }

    [HttpGet("search")]
    public async Task<IEnumerable<GameDto>> Search([FromQuery] string? search = null, [FromQuery] string? tags = null, [FromQuery] string? platforms = null,
        [FromQuery] string? catalogs = null, [FromQuery] string? developers = null, [FromQuery] string? publishers = null, [FromQuery] OrderingType ordering = OrderingType.Default,
        [FromQuery] int minRating = 0, [FromQuery] int maxRating = 100, [FromQuery] int minYear = 0, [FromQuery] int maxYear = 10000, [FromQuery] bool? dlc = null,
        [FromQuery] double minPrice = 0, [FromQuery] double maxPrice = 1000000, [FromQuery] bool? isReleased = null, [FromQuery] bool indexDLCs = false,
        [FromQuery] int gamesPerPage = 12, [FromQuery] int page = 1)
    {
        IQueryable<Game> request = _context.Games
            .Include(g => g.Publisher)
            .Include(g => g.Developer)
            .Include(g => g.Tags)
            .Include(g => g.Platforms)
            .Include(g => g.CatalogsLinks)
                .ThenInclude(l => l.Catalog);

        if (!string.IsNullOrEmpty(search))
        {
            search = search.ToUpper();
            request = request.Where(g => g.Title.ToUpper().Contains(search));
        }
        if (TryParseStringToIntArray(tags, out var tagsList))
        {
            request = _filterService.FilterByTags(request, tagsList);
        }
        if (TryParseStringToIntArray(platforms, out var platformsList))
        {
            request = _filterService.FilterByPlatforms(request, platformsList);
        }
        if (TryParseStringToIntArray(catalogs, out var catalogsList))
        {
            request = _filterService.FilterByCatalogs(request, catalogsList);
        }
        if (TryParseStringToIntArray(developers, out var developersList))
        {
            request = _filterService.FilterByDevelopers(request, developersList);
        }
        if (TryParseStringToIntArray(publishers, out var publishersList))
        {
            request = _filterService.FilterByPublishers(request, publishersList);
        }

        request = _filterService.FilterByRating(request, minRating, maxRating);
        request = _filterService.FilterByYear(request, minYear, maxYear);
        request = _filterService.FilterByPrice(request, minPrice, maxPrice);

        if (isReleased != null)
        {
            request = _filterService.FilterByReleaseStatus(request, isReleased.Value);
        }
        if (dlc != null)
        {
            request = _filterService.FilterByAvailabilityOfDLC(request, dlc.Value);
        }

        if (!indexDLCs)
        {
            request = _filterService.FilterOutDLCs(request);
        }

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

        gamesPerPage = Math.Max(1, gamesPerPage);
        page = Math.Max(1, page);
        request = request.Skip((page - 1) * gamesPerPage).Take(gamesPerPage);

        var games = await request.AsNoTracking().ToListAsync();
        return games.Select(GameDto.FromGame);
    }

    [HttpGet("game/{id}")]
    public async Task<GameDto?> GetGame(int id)
    {
        var game = await _context.Games
            .Include(g => g.Publisher)
            .Include(g => g.Developer)
            .Include(g => g.Tags)
            .Include(g => g.Platforms)
            .Include(g => g.CatalogsLinks).ThenInclude(l => l.Catalog)

            .Include(g => g.DLCs).ThenInclude(g => g.Publisher)
            .Include(g => g.DLCs).ThenInclude(g => g.Developer)
            .Include(g => g.DLCs).ThenInclude(g => g.Tags)
            .Include(g => g.DLCs).ThenInclude(g => g.Platforms)
            .Include(g => g.DLCs).ThenInclude(g => g.CatalogsLinks)

            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.Id == id);

        return game == null ? null : GameDto.FromGame(game);
    }

    [HttpGet("filters")]
    public async Task<FiltersDto> GetFilters()
    {
        var tags = await _context.Tags.ToListAsync();
        var platforms = await _context.Platforms.ToListAsync();
        var catalogs = await _context.Catalogs.ToListAsync();

        var developers = await _context.Companies.Where(c => c.DevelopedGames.Count != 0).ToListAsync();
        var publishers = await _context.Companies.Where(c => c.PublishedGames.Count != 0).ToListAsync();

        return new FiltersDto(
            tags.Select(t => new FilterDto(t.Id, t.Name)).ToList(),
            platforms.Select(p => new FilterDto(p.Id, p.Name)).ToList(),
            catalogs.Select(c => new FilterDto(c.Id, c.Name)).ToList(),
            developers.Select(d => new FilterDto(d.Id, d.Name)).ToList(),
            publishers.Select(p => new FilterDto(p.Id, p.Name)).ToList());
    }

    private static bool TryParseStringToIntArray(string? value, out int[] result)
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
