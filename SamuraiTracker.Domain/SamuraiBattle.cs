using SamuraiTracker.Domain.Enums;
using SamuraiTracker.Domain.Interfaces;
using System;

namespace SamuraiTracker.Domain
{
  public class SamuraiBattle : IObjectWithState
  {
    public int Id { get; set; }
    public int SamuraiId { get; set; }
    public int BattleId { get; set; }
    public DateTime DateJoined { get; set; }
    public ObjectState State { get; set; }
  }
}