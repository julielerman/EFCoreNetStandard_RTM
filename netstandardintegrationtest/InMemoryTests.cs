using EFCoreDbContext;
using Microsoft.EntityFrameworkCore;
using SamuraiTracker.Domain;
using SamuraiTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TestProject
{
  public class InMemoryTests

  {
    [Fact]
    public void BackwardsCompatible_DbSetAddAndAddRangeOnSingleObjects() {
      InstantiateSamurais();
      using (var context = new SamuraiContext(true)) {
        ResetContext(context);
        context.Samurais.Add(Samurai_KK);
        context.Samurais.AddRange(Samurai_KS, Samurai_S, Samurai_HH);
        context.SaveChanges();
        Assert.Equal(4, context.Samurais.Count());
      }
    }

    [Fact]
    public void New_DbContextAddAndAddRange_DetectsType() {
      InstantiateSamurais();
      using (var context = new SamuraiContext(true)) {
        ResetContext(context);
        context.Add(Samurai_KO);
        var quote = new Quote { Text = "oh my!" };
        context.AddRange(Samurai_GK, quote);
        context.SaveChanges();
        Assert.Equal(2, context.Samurais.Count());
        Assert.Equal(1, context.Quotes.Count());
      }
    }

    [Fact]
    public void StateViaAddWillOnlyBeAppliedToUnTrackedObjects() {
      using (var context = new SamuraiContext(true)) {
        var maker = new Maker();
        context.Makers.Add(maker);
        context.SaveChanges();
        //maker is now known and Unchanged
        var newSword = new Sword();
        newSword.Maker = maker;
        context.Swords.Add(newSword);
        Assert.Equal(EntityState.Added, context.Entry(newSword).State);

        Assert.Equal(EntityState.Unchanged, context.Entry(maker).State);
      }
    }
    [Fact]
    public void StateViaTrackGraphWillBeAppliedOnlyToUntrackedObjects() {
      using (var context = new SamuraiContext(true)) {

        var maker = new Maker();
        context.Makers.Add(maker);
        context.SaveChanges();
        //maker is now known and Unchanged
        var newSword = new Sword();
        newSword.Maker = maker;
        context.ChangeTracker.TrackGraph(newSword, e => e.Entry.State = EntityState.Added);
        Assert.Equal(EntityState.Added, context.Entry(newSword).State);

        Assert.Equal(EntityState.Unchanged, context.Entry(maker).State);
      }


    }
    [Fact]
    public void CanApplyEntityStateViaChangeTracker() {
      //Arrange
      Samurai_KK.State = ObjectState.Unchanged;
      var quote = new Quote();
      quote.State = ObjectState.Added;
      var sword = new Sword();
      sword.State = ObjectState.Modified;
      Samurai_KK.Quotes.Add(quote);
      Samurai_KK.Swords.Add(sword);
      //Act
      using (var context = new SamuraiContext(true)) {


        context.ChangeTracker.TrackGraph(Samurai_KK, ChangeTrackerHelpers.ConvertStateOfNode);
        var expected = new List<int> { 3, 2, 0 };
        var addedCount = context.ChangeTracker.Entries().Count(e => e.State == EntityState.Added);
        var modifiedCount = context.ChangeTracker.Entries().Count(e => e.State == EntityState.Modified);
        var unchangedCount = context.ChangeTracker.Entries().Count(e => e.State == EntityState.Unchanged);
        Assert.Equal(expected, new List<int> { addedCount, modifiedCount, unchangedCount });
      }
    }

    [Fact]
    public void CanReplaceObjectStateWithEntryState() {
      using (var context = new SamuraiContext(true)) {
        var maker = new Maker();
        context.Makers.Add(maker);
        context.SaveChanges();
        //maker is now known and Unchanged
        var newSword = new Sword();
        newSword.Maker = maker;
        context.ChangeTracker.TrackGraph(newSword, e => e.Entry.State = EntityState.Added);
        Assert.Equal(EntityState.Added, context.Entry(newSword).State);

        Assert.Equal(EntityState.Unchanged, context.Entry(maker).State);
      }
    }
    [Fact]
    public void CanAddEntityToContext() {
      var samurai = new Samurai();
      Assert.Equal(EntityState.Added, AddToSetEFCore(samurai));
    }

    private EntityState AddToSetEFCore(object entity) {
      using (var context = new SamuraiContext()) {
        context.Add(entity);
        return context.Entry(entity).State;
      }
    }

    [Fact]
    public void CanAddRangeOfTypesToContext() {
      var samurai = new Samurai();
      var sword = new Sword();
      var states = AddViaContext(new object[] { samurai, sword });
      Assert.Equal(2, states.Count(i => i == EntityState.Added));
    }

    private EntityState[] AddViaContext(object[] entities) {
      using (var context = new SamuraiContext()) {
        context.AddRange(entities);
        var _states = new List<EntityState>();
        foreach (var item in context.ChangeTracker.Entries()) {
          _states.Add(item.State);
        }
        return _states.ToArray();
      }
    }

    [Fact]
    public void New_DbSetUpdateSetsModified() {
      //Arrange
      InstantiateSamurais();
      //Act

      using (var context = new SamuraiContext(true)) {
        context.Samurais.Update(Samurai_KS);

        Assert.Equal(EntityState.Modified, context.Entry(Samurai_KS).State);
      }
    }

    [Fact]
    public void BackwardsCompatible_BasicLinqQueryingStillWorks() {
      InstantiateSamurais();
      using (var context = new SamuraiContext(true)) {
        //Arrange
        ResetContext(context);
        context.Samurais.AddRange(Samurai_KS, Samurai_S, Samurai_HH, Samurai_GK);
        context.SaveChanges();

        //Act
        var samurais_UpperCaseShi = context.Samurais.Where(s => s.Name.Contains("Shi")).ToList();
        //Assert
        Console.WriteLine("Samurais with Upper Case 'Shi' in name");
        samurais_UpperCaseShi.ForEach(s => Console.WriteLine(s.Name));
        Assert.Equal(2, samurais_UpperCaseShi.Count);
        Console.WriteLine("____________________");
        //Assert
        var samurai = context.Samurais.FirstOrDefault(s => s.Name.Contains("shi"));

        Assert.Equal(samurai.Name, "Heihachi Hayashida");
      }
    }

    [Fact]
    public void BackwardsCompatible_DbSetAddAndAddRangeOnGraphs() {
      InstantiateSamurais();
      Samurai_GK.Quotes.Add(new Quote { Text = "oh my!" });
      using (var context = new SamuraiContext(true)) {
        ResetContext(context);
        context.Samurais.Add(Samurai_GK);
        context.SaveChanges();
        Assert.Equal(1, context.Samurais.Count());
        Assert.Equal(1, context.Quotes.Count());
      }
    }

    //[Fact]
    //public void New_DisconnectedPatterns_DbSetAdd_EnumToSpecifyRootOnly() {
    //  InstantiateSamurais();

    //  Samurai_GK.Quotes.Add(new Quote { Text = "oh my!" });
    //  using (var context = new SamuraiContext(true)) {
    //    ResetContext(context);
    //    context.Samurais.Add(Samurai_GK); //<--this should NOT make Quote added
    //    context.SaveChanges();
    //    Assert.Equal(1, context.Samurais.Count());
    //    Assert.Equal(0, context.Quotes.Count());
    //  }
    //}

    //[Fact]
    //public void New_DisconnectedPatterns_DbSetAddRange_EnumToSpecifyRootOnly() {
    //  InstantiateSamurais();
    //  Samurai_GK.Quotes.Add(new Quote { Text = "oh my!" });
    //  using (var context = new SamuraiContext(true)) {
    //    ResetContext(context);
    //    context.Samurais.AddRange(new[] { Samurai_KK, Samurai_GK }, behavior: GraphBehavior.SingleObject);
    //    context.SaveChanges();
    //    Assert.Equal(2, context.Samurais.Count());
    //    Assert.Equal(0, context.Quotes.Count());
    //  }
    //}

    [Fact]
    public void New_DisconnectedPatterns_EntryAdd_AddsRootOnly() {
      InstantiateSamurais();
      Samurai_GK.Quotes.Add(new Quote { Text = "oh my!" });
      using (var context = new SamuraiContext(true)) {
        ResetContext(context);
        context.Entry(Samurai_GK).State = EntityState.Added;
        context.SaveChanges();
        Assert.Equal(1, context.Samurais.Count());
        Assert.Equal(0, context.Quotes.Count());
      }
    }

    private void ResetContext(SamuraiContext context) {
      context.Database.EnsureDeleted();
      context.Database.EnsureCreated();
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

    private Samurai Samurai_KK;
    private Samurai Samurai_KS;
    private Samurai Samurai_S;
    private Samurai Samurai_KO;
    private Samurai Samurai_HH;
    private Samurai Samurai_KZ;
    private Samurai Samurai_GK;
  }
}