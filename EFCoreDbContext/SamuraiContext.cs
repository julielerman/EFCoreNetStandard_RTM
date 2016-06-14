using Microsoft.EntityFrameworkCore;
using SamuraiTracker.Domain;

namespace EFCoreDbContext
{
      public class SamuraiContext: DbContext
    {
    public DbSet<Samurai> Samurais { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      modelBuilder.Entity<Maker>()
    .HasMany(m => m.Swords)
    .WithOne(s => s.Maker);

      //no longer need to specify the one to one betwee samurai & secret identity
      //efcore infers it and got it right 
      
    }
  }
}
