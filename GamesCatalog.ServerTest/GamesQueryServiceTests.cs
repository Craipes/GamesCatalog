using GamesCatalog.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace GamesCatalog.Server.Services.Tests
{
    [TestFixture]
    public class GamesQueryServiceTests
    {
        private GamesDbContext _context;
        private GamesQueryService _service;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<GamesDbContext>()
                .UseInMemoryDatabase(databaseName: "GamesCatalogTest")
                .Options;

            _context = new GamesDbContext(options);
            _service = new GamesQueryService(_context);

            // Seed the in-memory database with test data
            _context.Games.AddRange(new List<Game>
            {
                new Game { Id = 1, Title = "Game 1" },
                new Game { Id = 2, Title = "Game 2" },
                new Game { Id = 3, Title = "Game 3" },
                new Game { Id = 4, Title = "Game 4" },
                new Game { Id = 5, Title = "Game 5" },
                new Game { Id = 6, Title = "Game 6" },
                new Game { Id = 7, Title = "Game 7" },
                new Game { Id = 8, Title = "Game 8" },
                new Game { Id = 9, Title = "Game 9" },
                new Game { Id = 10, Title = "Game 10" }
            });
            _context.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public void GetGamesQuery_ShouldReturnQueryableGames()
        {
            // Act
            var result = _service.GetGamesQuery();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<IQueryable<Game>>());
        }

        [Test]
        public void GetGamesWithDLCsQuery_ShouldReturnQueryableGamesWithDLCs()
        {
            // Act
            var result = _service.GetGamesWithDLCsQuery();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<IQueryable<Game>>());
        }

        [TestCase(10, 1)]
        [TestCase(5, 2)]
        public void Paginate_ShouldReturnPaginatedGames(int gamesPerPage, int page)
        {
            // Act
            var result = _service.Paginate(_context.Games.AsQueryable(), gamesPerPage, page);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(gamesPerPage));
        }

        [TestCase(OrderingType.TitleAsc)]
        [TestCase(OrderingType.TitleDesc)]
        [TestCase(OrderingType.YearAsc)]
        [TestCase(OrderingType.YearDesc)]
        [TestCase(OrderingType.RatingAsc)]
        [TestCase(OrderingType.RatingDesc)]
        public void Order_ShouldReturnOrderedGames(OrderingType ordering)
        {
            // Act
            var result = _service.Order(_context.Games.AsQueryable(), ordering);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<IQueryable<Game>>());
        }
    }
}
