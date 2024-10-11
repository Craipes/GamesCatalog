namespace GamesCatalog.Server.Data;

public class Company : AttributeEntity
{
    public List<Game> DevelopedGames { get; set; } = [];
    public List<Game> PublishedGames { get; set; } = [];
}
