using GamesCatalog.Server.Services;

namespace GamesCatalog.Server.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class AppController : Controller
{
    private const int MinRating = 0;
    private const int MaxRating = 100;
    private const int MinYear = 0;
    private const int MaxYear = 10000;
    private const double MinPrice = 0;
    private const double MaxPrice = 1000000;

    private readonly IFilterService _filterService;
    private readonly IGamesQueryService _gamesQueryService;

    private readonly GamesDbContext _context;

    public AppController(IFilterService filterService, IGamesQueryService gamesQueryService, GamesDbContext context)
    {
        _filterService = filterService;
        _gamesQueryService = gamesQueryService;
        _context = context;
    }

    [HttpGet("search")]
    public async Task<IEnumerable<GameDto>> Search([FromQuery] string? search = null, [FromQuery] string? tags = null, [FromQuery] string? platforms = null,
        [FromQuery] string? catalogs = null, [FromQuery] string? developers = null, [FromQuery] string? publishers = null, [FromQuery] OrderingType ordering = OrderingType.Default,
        [FromQuery] int minRating = MinRating - 1, [FromQuery] int maxRating = MaxRating + 1, [FromQuery] int minYear = MinYear - 1, [FromQuery] int maxYear = MaxYear + 1, [FromQuery] bool? dlc = null,
        [FromQuery] double minPrice = MinPrice - 1, [FromQuery] double maxPrice = MaxPrice + 1, [FromQuery] bool? isReleased = null, [FromQuery] bool indexDLCs = false,
        [FromQuery] int gamesPerPage = 12, [FromQuery] int page = 1)
    {
        var request = _gamesQueryService.GetGamesQuery();

        if (!string.IsNullOrEmpty(search))
        {
            request = _filterService.FilterByTitle(request, search, true);
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

        if (minRating <= maxRating && (minRating >= MinRating || maxRating <= MaxRating))
        {
            request = _filterService.FilterByRating(request, minRating, maxRating);
        }
        if (minYear <= maxYear && (minYear >= MinYear || maxYear <= MaxYear))
        {
            request = _filterService.FilterByYear(request, minYear, maxYear);
        }
        if (minPrice <= maxPrice && (minPrice >= MinPrice || maxPrice <= MaxPrice))
        {
            request = _filterService.FilterByPrice(request, minPrice, maxPrice);
        }

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

        request = _gamesQueryService.Order(request, ordering);
        request = _gamesQueryService.Paginate(request, gamesPerPage, page);

        var games = await request.ToListAsync();
        return games.Select(GameDto.FromGame);
    }

    [HttpGet("game/{id}")]
    public async Task<GameDto?> GetGame(int id)
    {
        var game = await _gamesQueryService.GetGamesWithDLCsQuery().FirstOrDefaultAsync(g => g.Id == id);
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
            return false;
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
