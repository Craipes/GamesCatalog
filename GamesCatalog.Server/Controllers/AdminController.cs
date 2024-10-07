using GamesCatalog.Server.ViewModels;

namespace GamesCatalog.Server.Controllers
{
    public class AdminController : Controller
    {
        private readonly GamesDbContext _context;

        public AdminController(GamesDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Company()
        {
            return View("Attributes", await GetCompanyViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddCompany(Company company)
        {
            if (await AddAttribute(company)) return RedirectToAction(nameof(Company));
            return View("Attributes", await GetCompanyViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> EditCompany(Company company)
        {
            if (await EditAttribute(company)) return RedirectToAction(nameof(Company));
            return View("Attributes", await GetCompanyViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            if (await DeleteAttribute<Company>(id)) return RedirectToAction(nameof(Company));
            return View("Attributes", await GetCompanyViewModel());
        }

        private async Task<AttributesViewModel> GetCompanyViewModel() => new((await _context.Companies.ToListAsync()).AsReadOnly(), "Company");

        private async Task<bool> AddAttribute<T>(T attribute) where T : AttributeEntity
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

        private async Task<bool> EditAttribute<T>(T attribute) where T : AttributeEntity
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

        private async Task<bool> DeleteAttribute<T>(int id) where T : AttributeEntity
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

        private void SetErrorFromModelState()
        {
            ViewData["Error"] = "Invalid model state:\n" +
                string.Join('\n', ModelState.Values
                .SelectMany(e => e.Errors)
                .Select(e => e.ErrorMessage));
        }
    }
}
