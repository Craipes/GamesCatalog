using GamesCatalog.Server.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GamesCatalog.Server.Controllers;

public class GameController : Controller
{
    protected readonly GamesDbContext _context;

    public GameController(GamesDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        GamesViewModel model = new(_context.Games
            .Include(g => g.Publisher)
            .Include(g => g.Developer)
            .ToList().AsReadOnly());
        return View(model);
    }

    [HttpGet]
    public IActionResult Add()
    {
        GameManageViewModel model = new()
        {
            IsEditing = false,
            Game = new(),
            CompaniesSelectList = GetCompaniesSelectList(),
            GamesSelectList = GetGamesSelectList(null),
            Tags = _context.Tags.Select(t => new LinkCheckboxViewModel(t.Id, t.Name, false, null)).ToList(),
            Platforms = _context.Platforms.Select(t => new LinkCheckboxViewModel(t.Id, t.Name, false, null)).ToList(),
            CatalogsLinks = _context.Catalogs.Select(t => new LinkCheckboxViewModel(t.Id, t.Name, false, null)).ToList()
        };
        return View("Manage", model);
    }

    [HttpPost]
    public IActionResult Add(GameManageViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Error"] = "Invalid model state:\n" +
                string.Join('\n', ModelState.Values
                .SelectMany(e => e.Errors)
                .Select(e => e.ErrorMessage));
            model.CompaniesSelectList = GetCompaniesSelectList();
            model.GamesSelectList = GetGamesSelectList(null);
            return View("Manage", model);
        }

        Game game = new();
        if (!UpdateGameModel(model, game))
        {
            model.CompaniesSelectList = GetCompaniesSelectList();
            model.GamesSelectList = GetGamesSelectList(null);
            return View("Manage", model);
        }

        _context.AddRange(game!.CatalogsLinks!);
        _context.Add(game);

        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        Game? game = _context.Games
            .Include(g => g.Developer)
            .Include(g => g.Publisher)
            .Include(g => g.Tags)
            .Include(g => g.Platforms)
            .Include(g => g.CatalogsLinks)
            .FirstOrDefault(g => g.Id == id);

        if (game == null)
        {
            return NotFound();
        }

        var tags = _context.Tags.Select(t => new LinkCheckboxViewModel(t.Id, t.Name, false, null)).ToList();
        tags.ForEach(t => t.IsChecked = game.Tags!.Any(g => g.Id == t.Value));
        var platforms = _context.Platforms.Select(t => new LinkCheckboxViewModel(t.Id, t.Name, false, null)).ToList();
        platforms.ForEach(t => t.IsChecked = game.Platforms!.Any(g => g.Id == t.Value));
        var catalogsLinks = _context.Catalogs.Select(t => new LinkCheckboxViewModel(t.Id, t.Name, false, null)).ToList();
        catalogsLinks.ForEach(t =>
        {
            var link = game.CatalogsLinks!.FirstOrDefault(g => g.CatalogId == t.Value);
            if (link != null)
            {
                t.IsChecked = true;
                t.Link = link.Url;
            }
        });

        GameManageViewModel model = new()
        {
            IsEditing = true,
            Game = game,
            CompaniesSelectList = GetCompaniesSelectList(),
            GamesSelectList = GetGamesSelectList(game.Id),
            Tags = tags,
            Platforms = platforms,
            CatalogsLinks = catalogsLinks
        };
        return View("Manage", model);
    }

    [HttpPost]
    public IActionResult Edit(GameManageViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Error"] = "Invalid model state:\n" +
                string.Join('\n', ModelState.Values
                .SelectMany(e => e.Errors)
                .Select(e => e.ErrorMessage));
            model.CompaniesSelectList = GetCompaniesSelectList();
            model.GamesSelectList = GetGamesSelectList(model.Game.Id);
            return View("Manage", model);
        }

        var loadedGame = _context.Games
            .Include(g => g.Tags)
            .Include(g => g.Platforms)
            .Include(g => g.CatalogsLinks)
            .FirstOrDefault(g => g.Id == model.Game.Id);

        if (loadedGame == null || !UpdateGameModel(model, loadedGame))
        {
            model.CompaniesSelectList = GetCompaniesSelectList();
            model.GamesSelectList = GetGamesSelectList(model.Game.Id);
            return View("Manage", model);
        }

        _context.Update(loadedGame);

        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    private bool UpdateGameModel(GameManageViewModel model, Game game)
    {
        game.IsReleased = model.Game.IsReleased;
        game.Title = model.Game.Title;
        game.Description = model.Game.Description;
        game.YearOfRelease = model.Game.YearOfRelease;
        game.Rating = model.Game.Rating;
        game.Price = model.Game.Price;
        game.PreviewUrl = model.Game.PreviewUrl;
        game.ContentsUrls = model.Game.ContentsUrls;
        game.Requirements = model.Game.Requirements;

        if (model.Game.ParentGameId != null)
        {
            if (game.Id == model.Game.ParentGameId)
            {
                ViewData["Error"] = "Game can't be DLC of itself";
                return false;
            }
            game.ParentGame = _context.Games.Find(model.Game.ParentGameId);
            if (game.ParentGame == null)
            {
                ViewData["Error"] = "Invalid parent game. Try to reload the page";
                return false;
            }
            if (game.ParentGame.IsDLC)
            {
                ViewData["Error"] = "Game can't be DLC of a DLC";
                return false;
            }
        }
        else
        {
            game.ParentGameId = null;
        }

        game.Developer = _context.Companies.Find(model.Game.DeveloperId);
        if (game.Developer == null)
        {
            ViewData["Error"] = "Invalid developer. Try to reload the page";
            return false;
        }

        game.Publisher = _context.Companies.Find(model.Game.PublisherId);
        if (game.Publisher == null)
        {
            ViewData["Error"] = "Invalid publisher. Try to reload the page";
            return false;
        }

        var tags = model.Tags.Where(t => t.IsChecked).Select(t => _context.Tags.Find(t.Value)).ToList();
        if (tags.Any(t => t == null))
        {
            ViewData["Error"] = "Invalid tags. Try to reload the page";
            return false;
        }

        game.Tags ??= [];
        game.Tags.Clear();
        game.Tags.AddRange(tags!);

        var platforms = model.Platforms.Where(t => t.IsChecked).Select(t => _context.Platforms.Find(t.Value)).ToList();
        if (platforms.Any(t => t == null))
        {
            ViewData["Error"] = "Invalid platforms. Try to reload the page";
            return false;
        }

        game.Platforms ??= [];
        game.Platforms.Clear();
        game.Platforms.AddRange(platforms!);

        if (model.CatalogsLinks.Any(t => t.IsChecked && string.IsNullOrWhiteSpace(t.Link)))
        {
            ViewData["Error"] = "Empty catalog links are not allowed";
            return false;
        }

        var catalogsLinks = model.CatalogsLinks.Where(t => t.IsChecked)
            .Select(t => new CatalogLink() { Game = game, Catalog = _context.Catalogs.Find(t.Value), Url = t.Link! }).ToList();
        if (catalogsLinks.Any(t => t.Catalog == null))
        {
            ViewData["Error"] = "Invalid catalogs. Try to reload the page";
            return false;
        }

        game.CatalogsLinks ??= [];
        game.CatalogsLinks.Clear();
        game.CatalogsLinks.AddRange(catalogsLinks);

        return true;
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var game = _context.Games.Find(id);
        if (game != null && !_context.Games.Any(g => g.ParentGameId == game.Id))
        {
            _context.Remove(game);
            _context.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }

    private SelectList GetCompaniesSelectList()
    {
        return new SelectList(_context.Companies.ToList(), "Id", "Name");
    }

    private SelectList GetGamesSelectList(int? currentId)
    {
        return new SelectList(_context.Games
            .Where(g => g.Id != currentId)
            .Select(g => new
            {
                g.Id,
                g.Title
            }).ToList(), "Id", "Title");
    }
}
