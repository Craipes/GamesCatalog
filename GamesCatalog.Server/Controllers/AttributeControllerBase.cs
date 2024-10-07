using GamesCatalog.Server.ViewModels;

namespace GamesCatalog.Server.Controllers;
public abstract class AttributeControllerBase<K> : Controller where K: AttributeEntity
{
    protected readonly GamesDbContext _context;

    public AttributeControllerBase(GamesDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View("Attributes", await GetCompanyViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Add(K attribute)
    {
        if (await AddAttribute(attribute)) return RedirectToAction(nameof(Index));
        return View("Attributes", await GetCompanyViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Edit(K attribute)
    {
        if (await EditAttribute(attribute)) return RedirectToAction(nameof(Index));
        return View("Attributes", await GetCompanyViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        if (await DeleteAttribute<K>(id)) return RedirectToAction(nameof(Index));
        return View("Attributes", await GetCompanyViewModel());
    }

    private async Task<AttributesViewModel> GetCompanyViewModel() => new((await _context.Set<K>().ToListAsync()).AsReadOnly(), typeof(K).Name);

    protected async Task<bool> AddAttribute<T>(T attribute) where T : AttributeEntity
    {
        if (!ModelState.IsValid)
        {
            SetErrorFromModelState();
            return false;
        }

        try
        {
            if (await _context.Set<T>().AnyAsync(a => a.Name == attribute.Name))
            {
                ViewData["Error"] = "There is already attribute with this name";
                return false;
            }
            _context.Add(attribute);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            ViewData["Error"] = e.Message;
        }
        return false;
    }

    protected async Task<bool> EditAttribute<T>(T attribute) where T : AttributeEntity
    {
        if (!ModelState.IsValid)
        {
            SetErrorFromModelState();
            return false;
        }

        try
        {
            if (await _context.Set<T>().AnyAsync(a => a.Name == attribute.Name))
            {
                ViewData["Error"] = "There is already attribute with this name";
                return false;
            }
            _context.Update(attribute);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            ViewData["Error"] = e.Message;
        }
        return false;
    }

    protected async Task<bool> DeleteAttribute<T>(int id) where T : AttributeEntity
    {
        try
        {
            var attribute = await _context.FindAsync<T>(id);
            if (attribute == null)
            {
                ViewData["Error"] = "Attribute not found";
                return false;
            }
            _context.Remove(attribute);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            ViewData["Error"] = e.Message;
        }
        return false;
    }

    protected void SetErrorFromModelState()
    {
        ViewData["Error"] = "Invalid model state:\n" +
            string.Join('\n', ModelState.Values
            .SelectMany(e => e.Errors)
            .Select(e => e.ErrorMessage));
    }
}
