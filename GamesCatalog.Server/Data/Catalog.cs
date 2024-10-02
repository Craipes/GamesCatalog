namespace GamesCatalog.Server.Data;

public class Catalog : BaseEntity
{
    [MaxLength(64)] public string Name { get; set; } = string.Empty;
}
