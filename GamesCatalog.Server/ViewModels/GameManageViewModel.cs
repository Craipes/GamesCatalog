using Microsoft.AspNetCore.Mvc.Rendering;

namespace GamesCatalog.Server.ViewModels;

public class GameManageViewModel
{
    public required bool IsEditing { get; set; }
    public required Game Game { get; set; }
    public SelectList? CompaniesSelectList { get; set; }
    public required IReadOnlyList<LinkCheckboxViewModel> Tags { get; set; }
    public required IReadOnlyList<LinkCheckboxViewModel> Platforms { get; set; }
    public required IReadOnlyList<LinkCheckboxViewModel> CatalogsLinks { get; set; }
}
