using SamuraiTracker.Domain.Enums;
using SamuraiTracker.Domain.Interfaces;

namespace SamuraiTracker.Domain
{
  public class Sword : IObjectWithState
  {
    public int Id { get; set; }
    public int WeightGrams { get; set; }
    public int MakerId { get; set; }
    public Maker Maker { get; set; }
    public int SamuraId { get; set; }
    public ObjectState State { get; set; }
  }
}