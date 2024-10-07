namespace GamesCatalog.Server.Controllers;

public class CompanyController : AttributeControllerBase<Company>
{
    public CompanyController(GamesDbContext context) : base(context) { }
}

public class PlatformController : AttributeControllerBase<Platform>
{
    public PlatformController(GamesDbContext context) : base(context) { }
}

public class CatalogController : AttributeControllerBase<Catalog>
{
    public CatalogController(GamesDbContext context) : base(context) { }
}

public class TagController : AttributeControllerBase<Tag>
{
    public TagController(GamesDbContext context) : base(context) { }
}