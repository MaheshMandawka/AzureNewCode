using Microsoft.EntityFrameworkCore;

public class MediaContext : DbContext
{
    public DbSet<Item> Items { get; set; }
    public DbSet<Bookshelf> Bookshelves { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured == false)
        {
            optionsBuilder.UseSqlServer(
                "server=.;Database=Media;trusted_connection=true;");
            base.OnConfiguring(optionsBuilder);
        }
    }

    public MediaContext(DbContextOptions options)
        : base(options) { }

    public MediaContext()
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}