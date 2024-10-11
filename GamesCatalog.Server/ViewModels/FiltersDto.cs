namespace GamesCatalog.Server.ViewModels;

public record FiltersDto(IReadOnlyCollection<FilterDto> Tags, IReadOnlyCollection<FilterDto> Platforms, IReadOnlyCollection<FilterDto> Catalogs,
    IReadOnlyCollection<FilterDto> Developers, IReadOnlyCollection<FilterDto> Publishers);
