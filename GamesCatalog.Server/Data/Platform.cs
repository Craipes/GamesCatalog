using System.ComponentModel.DataAnnotations;

namespace GamesCatalog.Server.Data;

public class Platform : BaseEntity
{
    [MaxLength(64)] public string Name { get; set; } = string.Empty;
}
