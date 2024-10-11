namespace GamesCatalog.Server.Data;

public class Game : BaseEntity
{
    [Column(TypeName = "boolean")] public bool IsReleased { get; set; } = true;
    [MaxLength(100)] public string Title { get; set; } = string.Empty;
    [Column(TypeName = "decimal(8,2)")] [Range(0, 1000000)] public double Price { get; set; }
    [Column(TypeName = "smallint")] [Range(1500, 10000)] public int YearOfRelease { get; set; }
    [Column(TypeName = "tinyint")] [Range(0, 100)] public int Rating { get; set; }
    [MaxLength(5000)] public string Description { get; set; } = string.Empty;
    [MaxLength(1000)] public string Requirements { get; set; } = string.Empty;
    [MaxLength(500)] public string PreviewUrl { get; set; } = string.Empty;
    [MaxLength(5000)] public string ContentsUrls { get; set; } = string.Empty;

    public List<Tag> Tags { get; set; } = [];
    public List<Platform> Platforms { get; set; } = [];
    public List<CatalogLink> CatalogsLinks { get; set; } = [];
    public Company? Developer { get; set; }
    public int? DeveloperId { get; set; }
    public Company? Publisher { get; set; }
    public int? PublisherId { get; set; }

    public int? ParentGameId { get; set; }
    public Game? ParentGame { get; set; }
    public List<Game> DLCs { get; set; } = [];

    [NotMapped] public bool IsDLC => ParentGameId != null;
}
