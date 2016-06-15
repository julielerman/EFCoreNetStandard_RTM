using SamuraiTracker.Domain.Enums;
using SamuraiTracker.Domain.Interfaces;

namespace SamuraiTracker.Domain
{
  public class Location : IObjectWithState
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public ObjectState State { get; set; }
  }
}