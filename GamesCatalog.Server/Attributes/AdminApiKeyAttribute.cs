using Microsoft.AspNetCore.Mvc.Filters;

namespace GamesCatalog.Server.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AdminApiKeyAttribute : Attribute, IAuthorizationFilter
{
    private const string apiKeyHeader = "X-Api-Key";
    private const string targetApiKeyPath = "AdminApiKey";

    public AdminApiKeyAttribute() { }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.HttpContext.Request.Headers.TryGetValue(apiKeyHeader, out var apiKey))
        {
            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var targetApiKey = configuration[targetApiKeyPath];
            if (targetApiKey == apiKey)
            {
                return;
            }
        }

        context.Result = new UnauthorizedResult();
    }
}
