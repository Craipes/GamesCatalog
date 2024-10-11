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
    public IEnumerable<GameDto> Search([FromQuery] string? search = null, [FromQuery] string? tags = null, [FromQuery] string? platforms = null,
        [FromQuery] string? catalogs = null, [FromQuery] string? developer = null, [FromQuery] string? publisher = null, [FromQuery] OrderingType ordering = OrderingType.Default,
        [FromQuery] int minRating = 0, [FromQuery] int maxRating = 100, [FromQuery] int minYear = 0, [FromQuery] int maxYear = 10000, [FromQuery] bool? dlc = null)
    {
        return _context.Games
            .Include(g => g.Publisher)
            .Include(g => g.Developer)
            .Include(g => g.Tags)
            .Include(g => g.Platforms)
            .Include(g => g.CatalogsLinks)
                .ThenInclude(c => c.Catalog)
            .Select(g => GameDto.FromGame(g))
            .ToList();
    }
}
