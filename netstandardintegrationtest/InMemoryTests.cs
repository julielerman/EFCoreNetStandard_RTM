using System;
using SamuraiTracker.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;
using EFCoreDbContext;

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

    Samurai Samurai_KK;
    Samurai Samurai_KS;
    Samurai Samurai_S;
    Samurai Samurai_KO;
    Samurai Samurai_HH;
    Samurai Samurai_KZ;
    Samurai Samurai_GK;
  }
}




