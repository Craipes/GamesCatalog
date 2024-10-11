namespace GamesCatalog.Server.Data;

public class GamesDbContext : DbContext
{
    public DbSet<Company> Companies { get; set; } = null!;
    public DbSet<Platform> Platforms { get; set; } = null!;
    public DbSet<Catalog> Catalogs { get; set; } = null!;
    public DbSet<CatalogLink> CatalogLinks { get; set; } = null!;
    public DbSet<Tag> Tags { get; set; } = null!;
    public DbSet<Game> Games { get; set; } = null!;

    public GamesDbContext(DbContextOptions<GamesDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>().HasOne(g => g.Developer).WithMany(c => c.DevelopedGames).HasForeignKey(g => g.DeveloperId);
        modelBuilder.Entity<Game>().HasOne(g => g.Publisher).WithMany(c => c.PublishedGames).HasForeignKey(g => g.PublisherId);
        modelBuilder.Entity<Game>().HasMany(g => g.Platforms).WithMany();
        modelBuilder.Entity<Game>().HasMany(g => g.Tags).WithMany();
        modelBuilder.Entity<Game>().HasMany<Catalog>().WithMany().UsingEntity<CatalogLink>();
        modelBuilder.Entity<Game>().HasOne(g => g.ParentGame).WithMany(g => g.DLCs).HasForeignKey(g => g.ParentGameId);
    }
}