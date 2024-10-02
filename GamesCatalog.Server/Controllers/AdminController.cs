namespace GamesCatalog.Server.Controllers;

[ApiController]
[AdminApiKey]
[Route("[controller]")]
public class AdminController : Controller
{
    private readonly ILogger<AdminController> _logger;

    public AdminController(ILogger<AdminController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "Test")]
    public string Test()
    {
        return "Test";
    }
}
