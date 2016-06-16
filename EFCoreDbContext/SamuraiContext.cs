using Microsoft.EntityFrameworkCore;
using SamuraiTracker.Domain;
using SamuraiTracker.Domain.Interfaces;

namespace EFCoreDbContext
{
  public class SamuraiContext : DbContext
  {
    private bool _useInMemory;
    private string _sqlConnection = "Server = (localdb)\\mssqllocaldb; Database=EF7SamuraiConsole; Trusted_Connection=True; MultipleActiveResultSets = True;";

    public DbSet<Samurai> Samurais { get; set; }
    public DbSet<Maker> Makers { get; set; }
    public DbSet<Quote> Quotes { get; set; }
    public DbSet<SecretIdentity> Secrets { get; set; }
    public DbSet<Sword> Swords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      modelBuilder.Entity<Maker>()
    .HasMany(m => m.Swords)
    .WithOne(s => s.Maker);
      foreach (var entity in modelBuilder.Model.GetEntityTypes()) {
      
        modelBuilder.Entity(entity.Name).Ignore("State");
      }
     
      

      //no longer need to specify the one to one betwee samurai & secret identity
      //efcore infers it and got it right
    }

    public SamuraiContext(DbContextOptionsBuilder optionsBuilder)
      : base() { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
      if (_useInMemory) {
        optionsBuilder.UseInMemoryDatabase();
        return;
      }

      optionsBuilder
        .UseSqlServer(_sqlConnection, o => o.MaxBatchSize(40));
    }

    public SamuraiContext() {
    }

    public SamuraiContext(bool useInMemory) {
      _useInMemory = useInMemory;
    }
  }
}