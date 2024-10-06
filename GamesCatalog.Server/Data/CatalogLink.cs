namespace GamesCatalog.Server.Data;

public class CatalogLink
{
    public int CatalogId { get; set; }
    public Catalog? Catalog { get; set; }
    public int GameId { get; set; }
    public Game? Game { get; set; }
    [MaxLength(512)] public string Url { get; set; } = string.Empty;
}
