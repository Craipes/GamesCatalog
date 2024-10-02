using System.ComponentModel.DataAnnotations;

namespace GamesCatalog.Server.Data;

public class Company : BaseEntity
{
    [MaxLength(128)] public string Name { get; set; } = string.Empty;
}
