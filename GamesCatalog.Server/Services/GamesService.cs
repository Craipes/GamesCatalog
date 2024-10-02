namespace GamesCatalog.Server.Services;

public class GamesService
{
    private readonly GamesDbContext _context;

    public GamesService(GamesDbContext context)
    {
        _context = context;
    }

    public async Task<List<Game>> GetGamesAsync()
    {
        return await _context.Games
            .Include(g => g.Developer)
            .Include(g => g.Publisher)
            .Include(g => g.Platforms)
            .Include(g => g.Tags)
            .ToListAsync();
    }

    public async Task<Game?> GetGameAsync(int id)
    {
        return await _context.Games
            .Include(g => g.Developer)
            .Include(g => g.Publisher)
            .Include(g => g.Platforms)
            .Include(g => g.Tags)
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<Game> CreateGameAsync(Game game)
    {
        _context.Games.Add(game);
        await _context.SaveChangesAsync();
        return game;
    }

    public async Task<Game> UpdateGameAsync(Game game)
    {
        _context.Games.Update(game);
        await _context.SaveChangesAsync();
        return game;
    }

    public async Task<bool> DeleteGameAsync(int id)
    {
        var existingGame = await _context.Games.FindAsync(id);
        if (existingGame == null)
        {
            return false;
        }
        _context.Games.Remove(existingGame);
        await _context.SaveChangesAsync();
        return true;
    }
}
