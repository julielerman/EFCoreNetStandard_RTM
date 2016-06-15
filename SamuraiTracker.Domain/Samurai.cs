using SamuraiTracker.Domain.Enums;
using SamuraiTracker.Domain.Interfaces;
using System.Collections.Generic;

namespace SamuraiTracker.Domain
{
  public class Samurai : IObjectWithState
  {
    public Samurai() {
      Quotes = new List<Quote>();
      SamuraiBattles = new List<SamuraiBattle>();
      Swords = new List<Sword>();
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public List<SamuraiBattle> SamuraiBattles { get; set; }
    public List<Quote> Quotes { get; set; }
    public List<Sword> Swords { get; set; }
    public SecretIdentity SecretIdentity { get; set; }
    public ObjectState State { get; set; }
  }
}