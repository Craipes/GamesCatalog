namespace GamesCatalog.Server.ViewModels;

public record GameDto(string Title, int YearOfRelease, int Rating, string Description, string Requirements, string PreviewUrl, string Developer, string Publisher,
    IReadOnlyCollection<string> ContentUrls, IReadOnlyCollection<string> Tags, IReadOnlyCollection<string> Platforms, IReadOnlyCollection<CatalogLinkDto> CatalogsLinks)
{
    private static readonly char[] separators = ['\n', '\r'];
    public static GameDto FromGame(Game game)
    {
        return new GameDto(
            game.Title, game.YearOfRelease, game.Rating, game.Description, game.Requirements, game.PreviewUrl,
            game.Developer?.Name ?? string.Empty, game.Publisher?.Name ?? string.Empty,
            game.ContentsUrls.Split(separators, StringSplitOptions.RemoveEmptyEntries),
            game.Tags?.Select(t => t.Name).ToList() ?? [], game.Platforms?.Select(p => p.Name).ToList() ?? [],
            game.CatalogsLinks?.Select(c => new CatalogLinkDto(c.Catalog?.Name ?? string.Empty, c.Url)).ToList() ?? []);
    }
}
