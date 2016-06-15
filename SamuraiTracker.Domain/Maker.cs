using SamuraiTracker.Domain.Enums;
using SamuraiTracker.Domain.Interfaces;
using System.Collections.Generic;

namespace SamuraiTracker.Domain
{
  public class Maker : IObjectWithState
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Sword> Swords { get; set; }
    public ObjectState State { get; set; }
  }
}