using Xunit;
using System.Linq;
using SamuraiTracker.Domain;
using EFCoreDbContext;
using Microsoft.EntityFrameworkCore;

namespace TestProject
{

  

  public class DbDependentTests
  {
    
    [Fact]
    public void ShowBatchCrud() {
      CreateAndSeedDbForEagerLoad();
    }

    private void CreateAndSeedDbForEagerLoad() {
      InstantiateSamurais();
      Samurai_GK.Quotes.Add(new Quote { Text = "oh my!" });
      var aMaker = new Maker { Name = "A Maker" };
      Samurai_GK.Swords.Add(new Sword { Maker = aMaker, WeightGrams = 100 });
      using (var context = new SamuraiContext()) {
        ResetContext(context);
        context.Makers.Add(aMaker);
        context.Samurais.Add(Samurai_GK);
        context.SaveChanges();
      }
    }

    [Fact]
    public void BackwardsCompatible_EagerLoadingWithImprovements() {
      //Note: Explicit Loading is backlog for RTM
      //using database for these tests so inmemory doesn't populate context
      //Arrange
      CreateAndSeedDbForEagerLoad();

      //Act
      using (var context = new SamuraiContext()) {
        var samurai = context.Samurais.Include(s => s.Quotes)
                                      .FirstOrDefault();
        Assert.Equal(1, samurai.Quotes.Count);
      }
      using (var context = new SamuraiContext()) {
        var samurai = context.Samurais.Include(s => s.Swords)
                                  .Include(s => s.Quotes)
                                  .FirstOrDefault();
        Assert.Equal(1, samurai.Quotes.Count);
        Assert.Equal(1, samurai.Swords.Count);
      }


      using (var context = new SamuraiContext()) {
        var samurai = context.Samurais
          .Include(s => s.Swords).ThenInclude(s => s.Maker)
          .FirstOrDefault();

        Assert.Equal(1, samurai.Swords.Count);
        Assert.NotNull(samurai.Swords[0].Maker);

      }
    }
   
    [Fact]
    public void New_DisconnectedPatterns_AttachNewGraphViaChangeTracker() {
      InstantiateSamurais();
      Samurai_GK.Quotes.Add(new Quote { Text = "oh my!" });
      using (var context = new SamuraiContext()) {
        ResetContext(context);
        context.ChangeTracker.TrackGraph(Samurai_GK,
          e => e.Entry.State = EntityState.Added);
        context.SaveChanges();
        Assert.Equal(1, context.Samurais.Count());
        Assert.Equal(1, context.Quotes.Count());
      }
    }

    [Fact]
    public void BackwardsCompatible_DbSetAddToCollectionOnGraphs() {
      InstantiateSamurais();
      Samurai_GK.Quotes.Add(new Quote { Text = "oh my!" });
      using (var context = new SamuraiContext()) {
        ResetContext(context);
        context.Samurais.Add(Samurai_GK);
        context.SaveChanges();
        Assert.Equal(1, context.Samurais.Count());
        Assert.Equal(1, context.Quotes.Count());
      }
    }

   // [Fact]
   //// [ExpectedException(typeof(DbUpdateException))]
   // public void New_DbSetAdd_ThrowsBecausePrincipalIsIgnoredInGraph() {
   //   InstantiateSamurais();
     
   //     var quote=new Quote { Text = "oh my!" };
   //   quote.Samurai = Samurai_GK;
   //   using (var context = new SamuraiContext()) {
   //     ResetContext(context);
   //     context.Quotes.Add(quote); //, GraphBehavior.IncludeDependents);
   //     context.SaveChanges();
   //   }
   // }

    [Fact]
    public void New_DbSetAdd_OneToOneIncludesDependentOnGraphs() {
      InstantiateSamurais();
      //CANT THIS BE IN MEMORY WITHOUT SAVECHANGES?
      var secret = new SecretIdentity { RealName = "James Bond" };
      Samurai_GK.SecretIdentity = secret;
      using (var context = new SamuraiContext()) {
        ResetContext(context);
        context.Samurais.Add(Samurai_GK); //, GraphBehavior.IncludeDependents);
        context.SaveChanges();
        Assert.Equal(1, context.Samurais.Count());
        Assert.Equal(1, context.Secrets.Count());
      }
    }

 

    private void ResetContext(SamuraiContext context) {
      context.Database.EnsureDeleted();
      context.Database.Migrate();
    }

    private void InstantiateSamurais() {
      Samurai_KK = new Samurai { Name = "Kikuchiyo" };
      Samurai_KS = new Samurai { Name = "Kambei Shimada" };
      Samurai_S = new Samurai { Name = "Shichirōji" };
      Samurai_KO = new Samurai { Name = "Katsushirō Okamoto" };
      Samurai_HH = new Samurai { Name = "Heihachi Hayashida" };
      Samurai_KZ = new Samurai { Name = "Kyūzō" };
      Samurai_GK = new Samurai { Name = "Gorōbei Katayama" };
    }

    Samurai Samurai_KK;
    Samurai Samurai_KS;
    Samurai Samurai_S;
    Samurai Samurai_KO;
    Samurai Samurai_HH;
    Samurai Samurai_KZ;
    Samurai Samurai_GK;
  }
}




