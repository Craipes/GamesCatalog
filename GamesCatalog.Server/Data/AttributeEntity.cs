namespace GamesCatalog.Server.Data;

public abstract class AttributeEntity : BaseEntity
{
    [MaxLength(64)] public string Name { get; set; } = string.Empty;
}
